using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public partial class SamuraiBattleStat
    {
        public string Name { get; set; }
        public int? NumberOfBattle { get; set; }
        public string EarliestBattle { get; set; }
    }
}
