﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    public class SamuraiContextNoTracking : DbContext
    {
        public SamuraiContextNoTracking()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Horse> Horses { get; set; }
    }
}
