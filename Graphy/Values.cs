using System;
using System.Collections.Generic;
using System.Text;

namespace Graphy
{
    public static class Values
    {
        private const float Sqrt2 = 1.4142135623730951f;

        public static Node Value(float value) => (float x, float y) => value;

        /// <summary>
        /// Horizontal lineargradient producing 0 at -1 to 1 at 1
        /// </summary>
        /// <returns></returns>
        public static Node LinearGradient() => (float x, float y) => MathF.Max(0, MathF.Min(1f, (x + 1) / 2));

        public static Node RadialGradient() => (float x, float y) => MathF.Max(0, MathF.Min(1, 1 - MathF.Sqrt(x * x + y * y)));

        public static Node Circle() => (float x, float y) => (x * x + y * y) < Sqrt2 ? 1 : 0;

        public static Node Sine() => (float x, float y) => MathF.Sin(x) / 2 + .5f;
    }

    public static class Transform
    {
        public static Node Translate(this Node node, float translateX, float translateY) => (float x, float y) => node(x + translateX, y + translateY);
        public static Node Translate(this Node node, Node translateX, Node translateY) => (float x, float y) => node(x + translateX(x, y), y + translateY(x, y));

        public static Node TranslateX(this Node node, float translate) => (float x, float y) => node(x + translate, y);
        public static Node TranslateX(this Node node, Node translate) => (float x, float y) => node(x + translate(x, y), y);

        public static Node TranslateY(this Node node, float translate) => (float x, float y) => node(x, y + translate);
        public static Node TranslateY(this Node node, Node translate) => (float x, float y) => node(x, y + translate(x, y));

        public static Node Scale(this Node node, float scale) => (float x, float y) => node(x / scale, y / scale);
        public static Node Scale(this Node node, Node scale) => (float x, float y) =>
        {
            var s = scale(x, y);
            return node(x / s, y / s);
        };

        public static Node ScaleX(this Node node, float scale) => (float x, float y) => node(x / scale, y);
        public static Node ScaleX(this Node node, Node scale) => (float x, float y) => node(x / scale(x, y), y);

        public static Node ScaleY(this Node node, float scale) => (float x, float y) => node(x, y / scale);
        public static Node ScaleY(this Node node, Node scale) => (float x, float y) => node(x, y / scale(x, y));

        public static Node Rotate(this Node node, float angle) => (float x, float y) =>
        {
            var cos = (float)Math.Cos(angle);
            var sin = (float)Math.Sin(angle);

            var rx = cos * x - sin * y;
            var ry = -sin * x + cos * y;

            return node(rx, ry);
        };
    }
}
