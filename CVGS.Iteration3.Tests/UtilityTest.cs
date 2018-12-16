using CVGS.Models;
using CVGS.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVGS.Iteration3.Tests
{   
    [TestClass]
    public class UtilityTest
    {
        //Naming convention
        //METHOD_PARAMS_EXPECTED BEHAVIOUR
        [TestMethod]
        public void CalculateTaxes_ALAndCartTotalOf10_Returns10AndAHalf()
        {
            string prov = "AL";
            double cartTotal = 10;

            var result = SD.CalculateTaxes(prov, cartTotal);
            double[] expectedResult = { .5, 10.5 };
            Assert.AreEqual(10.5, result[1]);
        }
        [TestMethod]
        public void CalculateTaxes_ALAndCartTotalOf10_ReturnsHalfOf1()
        {
            string prov = "AL";
            double cartTotal = 10;

            var result = SD.CalculateTaxes(prov, cartTotal);
            double[] expectedResult = { .5, 10.5 };
            Assert.AreEqual(.5, result[0]);
        }
    }
}
