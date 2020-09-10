using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp;

namespace Test
{
    [TestClass]
    public class BizDataLogicTests
    {
        [TestMethod]
        public void AddMultipleSamuraisReturnsCorrectNumberOfInsertedRows() 
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddMultipleSamurais");
            using (var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinessDataLogic(context);
                var nameList = new string[] { "Kikuchiyo", "Kyuzo", "Rikchi" };
                var result = bizlogic.AddMultipleSamurais(nameList);
                Assert.AreEqual(nameList.Count(), result);
            }
        }
    }
}
