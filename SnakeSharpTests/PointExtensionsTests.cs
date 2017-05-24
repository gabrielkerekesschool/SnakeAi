using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeSharp.Tests
{
    [TestClass()]
    public class PointExtensionsTests
    {
        [TestMethod()]
        public void CalculateDistanceFromTest()
        {
            var p1 = new Point(-7, -4);
            var p2 = new Point(17, 6);

            Assert.AreEqual(p1.CalculateDistanceFrom(p2), 26);
            Assert.AreEqual(p2.CalculateDistanceFrom(p1), 26);

            var p3 = new Point(123, 200);
            var p4 = new Point(-234, 90);

            Assert.AreEqual(373.562578, Math.Round(p3.CalculateDistanceFrom(p4), 6));
            Assert.AreEqual(373.562578, Math.Round(p4.CalculateDistanceFrom(p3), 6));
        }
    }
}