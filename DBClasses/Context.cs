using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses
{
    public class Context : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public Context() : base("localsql")
        {
        }
    }
}
