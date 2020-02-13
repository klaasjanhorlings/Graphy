using System;
using System.Drawing;

namespace Graphy.Test
{
    class Renderer
    {
        public RectangleF Bounds = new RectangleF(-1, -1, 2, 2);

        public void Render(Node node, Bitmap bitmap)
        {
            var w = bitmap.Width;
            var h = bitmap.Height;
            var sx = Bounds.Width / w;
            var sy = Bounds.Height / h;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var rx = Bounds.Left + x * sx;
                    var ry = Bounds.Top + y * sy;
                    var value = node(rx, ry);
                    var valueByte = (int)(MathF.Max(0f, MathF.Min(1f, value)) * 0xFF) & 0xFF;

                    bitmap.SetPixel(x, y, Color.FromArgb(valueByte, valueByte, valueByte));
                }
            }
        }
    }
}
