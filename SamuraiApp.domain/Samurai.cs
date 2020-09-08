using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Samurai
    { //Branch 3
        public Samurai()
       {
           Quotes = new List<Quote>();
       }
     public List<SamuraiBattle> SamuraiBattles { get; set; }
     public int Id { get; set; }
     public string Name { get; set; }
     public List<Quote> Quotes { get; set; }
     public Clan Clan { get; set; }
     public Horse Horse { get; set; }
    }
}
