using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ConsoleApp;

namespace ConsoleAppg
{
    public class Program
    {

        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            GetSamurais("Before Add: ");
            //AddSamurai();
            //GetSamurais("After Add: ");
            //InsertVariousType();
            //GetSamuraiSimpler();
            //Console.Write("Press Any Key.... ");
            //Console.ReadKey();
            InsertMultipleSamurais();
            QuerySamuraiBattleStats();
            QueryFilters();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach ( var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
        private static void InsertMultipleSamurais()
        {
            /* var samurai = new Samurai { Name = "Sampson" };
            var samurai2 = new Samurai { Name = "Tasha" };
            var samurai3 = new Samurai { Name = "Yasuo" };
            var samurai4 = new Samurai { Name = "Yone" }; */
            // _context.Samurais.AddRange(samurai, samurai2, samurai3, samurai4);
            // _context.SaveChanges();
            var _bizdata = new BusinessDataLogic();
            var samuraiNames = new string[] { "Sanpson", "Tasha", "Yasuo", "Yone" };
            var NewSamuraiCreated = _bizdata.AddMultipleSamurais(samuraiNames);

        }
        private static void InsertVariousType()
        {
            var samurai = new Samurai { Name = "Kikuchio" };
            var clan = new Clan { ClanName = "Imperial Clan" };
            _context.AddRange(samurai, clan);
            _context.SaveChanges();
        }
        private static void GetSamuraiSimpler()
        {
            //var samurais = context.Samurais.ToList();
            var query = _context.Samurais;
            //var samurai = query.ToList();
            foreach (var samurai in query)
            {
                Console.WriteLine(samurai.Name);
            }
        }
        private static void QueryFilters()
        {
            var name = "Sampson";
            //var samurais = _context.Samurai.FirstOrDefault(s => s.Name == name);
            //var samurais = _context.Samurai.Find(2);
            //var filter = "J%";
            //var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "J%")).ToList();
            var last = _context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == name);
        }
        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurai = _context.Samurais.Skip(1).Take(3).ToList();
            samurai.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }
        private static void MultipleDatabaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai { Name = "Kikuchiyo" });
            _context.SaveChanges();
        }
        private static void RetriveAndDeleteSamurai()
        {
            var samurai = _context.Samurais.Find(18);
            _context.Samurais.Remove(samurai);
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
        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote {Text = "I've come to save you" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void InsertNewSamuraiWithManyQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote> 
                {
                    new Quote {Text = "Watch out for my sharp sword!"},
                    new Quote {Text = "I told you to watch out for the sharp sword! Oh well!"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?"
            });
            using ( var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }
        private static void AddQuoteToExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }
        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }
        private static void ProjectSamuraisWithQuotes()
        {
            var samuraiWithHappyQuotes = _context.Samurais.Select(s => new
            {
                Samurai = s,
                HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy"))
            }).ToList();
        }
        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }
        public struct IdAndName
        {
            public int Id;
            public string Name;
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
        private static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Julie"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horses).Load();
        }
        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais.Where(s => s.Quotes.Any(q => q.Text.Contains("happy"))).ToList();
        }
        public static void LazyLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Julie"));
            var quoteCount = samurai.Quotes.Count();
        }
        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            samurai.Quotes[0].Text = "Did you hear that?";
            _context.Quotes.Remove(samurai.Quotes[2]);
            _context.SaveChanges();
        }
        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 3 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }
        private static void EnlistSamuraiIntoABattle()
        {
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattles
                .Add(new SamuraiBattle { SamuraiId = 21 });
            _context.SaveChanges();
        }
        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            var quote = samurai.Quotes[0];
            quote.Text += "Did you hear that again??";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }
        private static void RemovedJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 2 };
            _context.Remove(join);
            _context.SaveChanges();
        }
        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.Horses = new Horse { Name = "Silver" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddNewHorseToSamuraiUsingId()
        {
            var horse = new Horse { Name = "Scout", SamuraiId = 2 };
            _context.Add(horse);
            _context.SaveChanges();
        }
        private static void ReplaceAHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horses).FirstOrDefault(s => s.Id == 23);
            samurai.Horses = new Horse { Name = "Trigger" };
            _context.SaveChanges();
        }
        private static void AddNewHorseToSamuraiObject()
        {
            var samurai = _context.Samurais.Find(22);
            samurai.Horses = new Horse { Name = "Black Beauty" };
            _context.SaveChanges();
        }

        private static void AddNewHorseToDisconnectedSamuraiObject()
        {
            var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s => s.Id == 23);
            samurai.Horses = new Horse { Name = "Mr. Ed" };
            using (var newContext = new SamuraiContext())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }
        private static void GetSamuraiWithClan()
        {
            var samurai = _context.Samurais.Include(s => s.Clans).FirstOrDefault();
        }
        private static void GetClanWithSamurais()
        {
            var clan = _context.Clans.Find(3);
            var samuraiForClan = _context.Samurais.Where(s => s.Clans.Id == 3).ToList();
        }
        private static void GetSamuraisWithHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horses).ToList();
        }
        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(3);
            var horseWithSamurai = _context.Samurais.Include(s => s.Horses)
            .FirstOrDefault(s => s.Horses.Id == 3);

            var horseWithSamurais = _context.Samurais
            .Where(s => s.Horses != null)
            .Select(s => new { Horse = s.Horses, Samurai = s })
            .ToList();
        }
        private static void QuerySamuraiBattleStats()
        {
            var stats = _context.SamuraiBattleStats.ToList();           
        }
        private static void QureyUsingRawSql()
        {
            var samurais = _context.Samurais.FromSqlRaw("Select Id, Name, ClanId from Samurais").Include(s => s.Quotes).ToList();
        }
        private static void InterpolatedRawSqlQueryStoredProc()
        {
            var text = "Happy";
            var samurais = _context.Samurais.FromSqlInterpolated($"EXEC dbo.SamuraisWhoSaidAWord {text}").ToList(); 
        }
        private static void QueryUsingFromRawSqlStoredProc()
        {
            var text = "Happy";
            var samurais = _context.Samurais.FromSqlRaw(
            "EXEC dbo.SamuraisWhoSaidAWord {0}, text").ToList();
        }
        private static void ExecuteSomeRawSql()
        {
            var samuraiId = 22;
            samuraiId = 31;
            _context.Database.ExecuteSqlInterpolated($"EXEC DeleteQuotesForSamurai {samuraiId}");
        }
    }
}
