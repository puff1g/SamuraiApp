using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
namespace Test
{
    [TestClass]
    public class InMemoryTests
    {
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase() 
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using (var context = new SamuraiContext())
            {
                // context.Database.EnsureDeleted();
                // context.Database.EnsureCreated();
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                // Assert.AreNotEqual(0, samurai.Id);
                Assert.AreEqual(EntityState.Added, context.Entry(samurai).State);
            }
        }
    }
}
