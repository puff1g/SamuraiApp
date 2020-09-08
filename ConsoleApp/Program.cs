using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace ConsoleApp
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
        private static void InsertNewSamuraiWithAQuote()
            {
        var samurai = new Samurai 
                {
        Name = "Kambei Shimada",
        Quotes = new List<Quote>
        {
            new Quote { Text = "I've come to save you" }
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
                    Quotes = new List<Quote> {
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
            var someProperties = _context.Samurais.Select(s => new {s.Id, s.Name}).ToList();
            var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }
        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
            Id = id;
            Name = name;
            }
        }
        public int Id;
        public string Name;

        private static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Julie"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horses).Load();
        }

        public static void LazyLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Julie"));
            var quoteCount = samurai.Quotes.Count();
        }
        private static void FilteringWithRelatedData()
            {
                var samurais = _context.Samurais.Where(s => s.Quotes.Any(q => q.Text.Contains("happy"))).ToList();
            }
        
        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            samurai.Quotes[0].Text = "Did you hear that?";
            _context.Quotes.Remove(samurai.Quotes[2]);
            _context.SaveChanges();
        }
        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s =>s.Id==2);
            var quote = samurai.Quotes[0];
            quote.Text += "Did you hear that again??";
            using (var newContext = new SamuraiContext())
            {
                // newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
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

        private static void RemovedJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 2 };
            _context.Remove(join);
            _context.SaveChanges();
        }
        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattle = _context.Samurais
            .Include(s => s.SamuraiBattles)
            .ThenInclude(sb => sb.Battle)
            .FirstOrDefault(samurai=>samurai.Id==2);
            var samuraiWithBattlesCleaner = _context.Samurais.Where(s => s.Id == 2)
            .Select(s => new
            {
                Samurai = s,
                Battles = s.SamuraiBattles.Select(sb => sb.Battle)
            })
            .FirstOrDefault();
        }

        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jina Ujichika"};
            samurai.Horse = new Horse { Name = "Silver"};
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddNewHorseToSamuraiObject()
            {
        var samurai = _context.Samurais.Find(22);
        samurai.Horse = new Horse { Name = "Black Beauty"};
        _context.SaveChanges();
        }

        private static void ReplaceAHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horses).FirstOrDefault(s => s.Id == 23);
            samurai.Horses = new Horse { Name = "Trigger" };
            _context.SaveChanges();
        }

        private static void AddNewHorseToDisconnectedSamuraiObject()
        {
            var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s=>s.Id==23);
            samurai.Horse = new Horse { Name = "Mr. Ed"};
            using (var newContext=new SamuraiContext())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        private static void GetSamuraisWithHorse()
        {
            var samurai = _context.Samruais.Include(s => s.Horse).ToList();
        }

        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(3);
            var horseWithSamurai = _context.Samurais.Include(s => s.Horse)
            .FirstOrDefault(s => s.Horse.Id == 3);

            var horseWithSamurais = _context.Samurais
            .Where(s => s.Horse != null)
            .Select(s => new { Horse = s.Horse, Samurai = s})
            .ToList();
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


        }
}

