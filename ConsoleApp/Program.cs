using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace ConsoleApp1
{
   internal class Program
    {
        public static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            /*GetSamurais("Before Add:");
            AddSamurai();
            GetSamurais("After Add:");
            Console.WriteLine("Press anykey...");
            Console.ReadKey(); */
            InsertMultipleSamurais();
            GetSamuraisSimpler();
        }
        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Sampson" };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }
        
        private static void GetSamurais(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach(var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void InsertMultipleSamurais()
            {
            var samurai = new Samurai {Name = "Sampson"};
            var samurai2 = new Samurai { Name = "Tasha"};
            var samurai3 = new Samurai { Name = "MyHoe"};
            var samurai4 = new Samurai { Name = "Bitch"};
            context.Samurais.AddRange(samurai, samurai2);

            context.SaveChanges();
        }
        private static void InsertVariousTypes()
        {
            var samurai = new Samurai {Name = "Kikuchio"};
            var clan = new Clean {ClanNae = "Imperial Clan"};
            context.AddRange(samurai, clan);
            context.SaveChanges();
        }

        private static void GetSamuraisSimpler()
            {
        //var samurais = context.Samurais.ToList();
        var qurey = context.Samurais;
        var samurais = qurey.ToList();
            foreach (var samurai in qurey)
                {
            Console.WriteLine(samurai.Name);
            }
        }
        private static void QureyFilters()
            {
            var name = "Sampson";
            //var samurais = _context.Samurais.Where(samurais => s.Name == name).FirstOrDefault();
            //var samurai = _context.Samurais.Find(2);
            var last = _context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == name);
        }
        private static void RetriveAndUpdateSamurai()
            {
        //var samurai = _context.Samurais.FirstOrDefault();
        var samurai = _context.Samurais.Skip(1).Take(3).ToList();
            //samurai.Name += "San";
            samurais.ForEach(s=>s.Name += "San");
            _context.SaveChanges();
        }
        private static void MultipleDatabaseOperations()
            {
        var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai { Name = "Kikuchiyo"});
        }
        private static void RetriveAndDeleteSamurai()
            {
        var samurai = _context.Samurais.Find(18);
            _context:Samurais.Remove(samurai);
            _context.SaveChanges();
        }
        private static void InsertBattle()
        {
        _context.Battles.Add(new Battle
        {
            Name = "Battle of Okehazama",
            StartDate = new DateTime(1560, 05, 01),
            EndDate = new DateTime(1560, 06, 15),
        });
        _context.SaveChanges();
        }
        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }
    }
}
