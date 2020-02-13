using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graphy.Test
{
    [TestClass]
    public class TransformTests: TestBase
    {
        [TestMethod]
        public void TranslateX()
        {
            var checkers = Values.Checkers().Scale(0.3f);
            var noise = Noise.SimplexNoise().Scale(0.4f).Multiply(0.2f);
            var node = checkers.TranslateX(noise);

            Render(checkers, "TranslateX_checkers.png");
            Render(noise, "TranslateX_noise.png");
            Render(node, "TranslateX_node.png");
        }

        [TestMethod]
        public void Scale()
        {
            var checkers = Values.Checkers();
            var rotation = Values.RadialGradient();
            var node = checkers.Scale(rotation);

            Render(checkers, "Rotate_checkers.png");
            Render(rotation, "Rotate_rotation.png");
            Render(node, "Rotate_node.png");
        }
    }
}
