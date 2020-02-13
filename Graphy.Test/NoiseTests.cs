using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graphy.Test
{
    [TestClass]
    public class NoiseTests: TestBase
    {
        [TestMethod]
        public void SimplexNoise()
        {
            var node = Noise.SimplexNoise();
            Render(node, "SimplexNoise.png");
        }
    }
}
