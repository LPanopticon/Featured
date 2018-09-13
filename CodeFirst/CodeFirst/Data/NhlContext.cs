﻿using CodeFirst.Models.NHL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirst.Data
{
    public class NhlContext : DbContext
    {
        public NhlContext() : base("DefaultConnection")
        { }

        public DbSet<Team> Teams{ get; set; }
        public DbSet<Player> Players { get; set; }
    }
}