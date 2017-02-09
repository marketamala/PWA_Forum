using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace App.Models
{
    public class DataCollection : DbContext
    {
        public DbSet<Accounts> Accounts { get; set; }

        public DbSet<Themes> Themes { get; set; }

        public DbSet<Posts> Posts { get; set; }

        public DbSet<Responses> Responses { get; set; }
    }
}