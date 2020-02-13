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

        public static Node Checkers() => (float x, float y) => (MathF.Floor(x) % 2 == 0) ^ (MathF.Floor(y) % 2 == 0) ? 1f : 0f;
    }
}
