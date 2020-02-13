using System;

namespace Graphy
{
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

        public static Node Rotate(this Node node, Node angle) => (float x, float y) =>
        {
            var a = angle(x, y);
            var cos = (float)Math.Cos(a);
            var sin = (float)Math.Sin(a);

            var rx = cos * x - sin * y;
            var ry = -sin * x + cos * y;

            return node(rx, ry);
        };
    }
}
