using Graphy.NoiseAlgorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graphy
{
    public static class Noise
    {
        public static Node SimplexNoise()
        {
            var noise = new SimplexNoise();
            return (float x, float y) => noise.Evaluate(x, y) / 2 + .5f;
        }

        public static Node Octaves(this Node node, int octaves, Action<OctavesConfiguration> configure = null)
        {
            var config = new OctavesConfiguration();
            configure?.Invoke(config);

            var amplitude = 1f;
            var frequency = 1f;
            var sum = 0f;
            Node result = null;
            for(var i = 0; i < octaves; i++)
            {
                var offsetX = (float)(config.Random.NextDouble() * 10);
                var offsetY = (float)(config.Random.NextDouble() * 10);

                var octave = node
                    .Translate(offsetX, offsetY)
                    .Scale(1 / frequency)
                    .Multiply(amplitude);

                result = result?.Add(octave) ?? octave;

                sum += amplitude;
                amplitude *= config.Persistence;
                frequency *= config.Lacunarity;
            }

            return result.Divide(sum);
        }
    }

    public class OctavesConfiguration
    {
        private float persistence = 0.6f;
        private float lacunarity = 2f;
        private Random random = new Random();

        public float Persistence
        {
            get => persistence;
            set => persistence = value > 0 ? value : throw new ArgumentException($"{nameof(persistence)} can't be lower or equal to 0");
        }

        public float Lacunarity
        {
            get => lacunarity;
            set => lacunarity = value > 0 ? value : throw new ArgumentException($"{nameof(lacunarity)} can't be lower or equal to 0");
        }

        public Random Random
        {
            get => random;
            set => random = value ?? throw new ArgumentNullException(nameof(Random));
        }

        internal OctavesConfiguration() { }
    }
}
