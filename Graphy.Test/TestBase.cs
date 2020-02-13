using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Graphy.Test
{
    public class TestBase
    {
        public const int Width = 512;
        public const int Height = 512;

        protected void Render(Node node, string path)
        {
            using var bitmap = new Bitmap(Width, Height);

            var renderer = new Renderer();
            renderer.Render(node, bitmap);

            bitmap.Save(path);
        }
        private int ToByte(float value) => (int)(MathF.Max(0, MathF.Min(1, value)) * 0xFF) & 0xFF;
    }
}
