using System;

namespace Graphy
{
    public static class Arithmetic
    {
        public static Node Add(this Node node, float addend) => (float x, float y) => node(x, y) + addend;
        public static Node Add(this Node node, Node addend) => (float x, float y) => node(x, y) + addend(x, y);

        public static Node Subtract(this Node node, float subtrahend) => (float x, float y) => node(x, y) - subtrahend;
        public static Node Subtract(this Node node, Node subtrahend) => (float x, float y) => node(x, y) - subtrahend(x, y);

        public static Node Multiply(this Node node, float multiplier) => (float x, float y) => node(x, y) * multiplier;
        public static Node Multiply(this Node node, Node multiplier) => (float x, float y) => node(x, y) * multiplier(x, y);

        public static Node Divide(this Node node, float divider) => (float x, float y) => node(x, y) / divider;
        public static Node Divide(this Node node, Node divider) => (float x, float y) => node(x, y) / divider(x, y);

        public static Node Clamp(this Node node, float min = 0, float max = 1) => (float x, float y) => MathF.Max(min, MathF.Min(max, node(x, y)));

        public static Node Lerp(this Node node, Node other, float ratio) => (float x, float y) => node(x, y) * ratio + other(x, y) * (1 - ratio);

        public static Node Lerp(this Node node, Node other, Node ratio) => (float x, float y) =>
        {
            var r = ratio(x, y);
            return node(x, y) * r + other(x, y) * (1 - r);
        };
    }
}
