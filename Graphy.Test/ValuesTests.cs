using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Graphy.Test
{
    [TestClass]
    public class ValuesTests: TestBase
    {
        [TestMethod]
        public void LinearGradient()
        {
            var node = Values.LinearGradient();
            Render(node, "LinearGradient.png");
        }

        [TestMethod]
        public void RadialGradient()
        {
            var node = Values.RadialGradient();
            Render(node, "RadialGradient.png");
        }
    }
}
