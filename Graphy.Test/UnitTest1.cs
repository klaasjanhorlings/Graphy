using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace Graphy.Test
{
    [TestClass]
    public class UnitTest1
    {
        private Node Heightmap {
            get 
            {
                var rng = new Random(1337);

                var heightmap = Noise.SimplexNoise().Octaves(6, x => {
                    x.Persistence = 0.5f;
                    x.Lacunarity = 1.8f;
                    x.Random = new Random(123);
                });

                var offset = Noise.SimplexNoise().Octaves(2);
                var offsetX = offset.Translate((float)rng.NextDouble(), (float)rng.NextDouble())
                    .Subtract(.5f)  // [0..1] -> [-0.5..0.5]
                    .Multiply(.8f); // -> [-0.4..0.4];
                var offsetY = offset.Translate((float)rng.NextDouble(), (float)rng.NextDouble())
                    .Subtract(.5f)  // [0..1] -> [-0.5..0.5]
                    .Multiply(.8f); // -> [-0.4..0.4];

                var mask = Values.RadialGradient()
                                    .Multiply(3f)
                                    .Clamp()
                                    .Scale(.7f)
                                    .Translate(offsetX, offsetY);
                return heightmap.Lerp(Values.Value(0), mask);
            }
        }

        [TestMethod]
        public void RenderHeightmap()
        {
            using (var bitmap = new Bitmap(1024, 1024))
            {
                var renderer = new Renderer();
                renderer.Render(Heightmap, bitmap);
                //renderer.Render(mask, bitmap);
                bitmap.Save($"result.png");
            }
        }

        [TestMethod]
        public void Erosion()
        {
            var rng = new Random(123);
            var heightmap = Heightmap;
            var columns = 512;
            var rows = 512;

            var heights = new float[columns, rows];
            var visited = new bool[columns, rows];
            for(var y = 0; y < rows; y++)
            {
                for(var x = 0; x < columns; x++)
                {
                    heights[x, y] = heightmap((float) x / columns * 2 - 1, (float) y / rows * 2 - 1);
                }
            }

            var sediment = 0f;
            for(var d = 0; d < 1000; d++)
            {
                var dropletX = (int)(rng.NextDouble() * columns);
                var dropletY = (int)(rng.NextDouble() * rows);

                for(var i = 0; i < 50; i++)
                {
                    visited[dropletX, dropletY] = true;

                    if (dropletX == 0 || dropletX == columns - 1 || dropletY == 0 || dropletY == rows - 1)
                    {
                        break;
                    }

                    var currentHeight = heights[dropletX, dropletY];
                    var lowest = currentHeight;
                    var lowestX = dropletX;
                    var lowestY = dropletY;

                    for (var y = -1; y <= 1; y++)
                    {
                        for (var x = -1; x <= 1; x++)
                        {
                            if (y == 0 && x == 0)
                            {
                                continue;
                            }

                            var height = heights[dropletX + x, dropletY + y];
                            if (height < lowest)
                            {
                                lowest = height;
                                lowestX = dropletX + x;
                                lowestY = dropletY + y;
                            }
                        }
                    }

                    if (lowestX == dropletX && lowestY == dropletY)
                    {
                        break;
                    }

                    var deltaY = currentHeight - lowest;
                    var deltaX = (dropletX != lowestX && dropletY != lowestY) ? MathF.Sqrt(2) : 1;
                    deltaX /= 1000;
                    var angle = MathF.Atan(deltaY / deltaX);

                    dropletX = lowestX;
                    dropletY = lowestY;
                }
            }

            var w = heights.GetLength(1);
            var h = heights.GetLength(0);
            using (var bitmap = new Bitmap(w, h))
            {
                for (var y = 0; y < w; y++)
                {
                    for (var x = 0; x < h; x++)
                    {
                        var value = (int)(heights[x, y] * 0xff) & 0xff;
                        bitmap.SetPixel(x, y, Color.FromArgb(visited[x,y] ? 0xff : value, value, value));
                    }
                }

                bitmap.Save($"erosion.png");
            }
        }
    }
}
