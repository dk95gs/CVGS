using CVGS.Models;
using CVGS.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CVGS.Iteration2.Tests
{   
    [TestClass]
    public class UtilityTest
    {
        //Naming convention
        //METHOD_PARAMS_EXPECTED BEHAVIOUR
        [TestMethod]
        public void CalculateRating_GenericListOfRatings_Returns5()
        {
            List<Ratings> ratings = new List<Ratings>
            {
                new Ratings{ ApplicationUserId = "dsadas", GameId = 1, Id = 1, Rating = 5},
                new Ratings{ ApplicationUserId = "dsadas", GameId = 1, Id = 1, Rating = 5},
                new Ratings{ ApplicationUserId = "dsadas", GameId = 1, Id = 1, Rating = 5},
                new Ratings{ ApplicationUserId = "dsadas", GameId = 1, Id = 1, Rating = 5},
                new Ratings{ ApplicationUserId = "dsadas", GameId = 1, Id = 1, Rating = 5}
            };

            var result = SD.CalculateRating(ratings);

            Assert.AreEqual(result, 5);
        }
        [TestMethod]
        public void Encrypt_TestAndTestKey_ReturnsHashOfTest()
        {
            var result = EncryptDecrypt.Encrypt("Test", "TestKey");

            Assert.AreEqual(result, "PWpIf0oJFxOuKBDYKpd6QA==");
        }
        [TestMethod]
        public void Decrypt_HashOfTestAndTestKey_ReturnsTest()
        {
            var result = EncryptDecrypt.Decrypt("PWpIf0oJFxOuKBDYKpd6QA==", "TestKey");

            Assert.AreEqual(result, "Test");
        }
    }
}
