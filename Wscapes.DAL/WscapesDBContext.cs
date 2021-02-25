using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wscapes.DAL.Models;

namespace Wscapes.DAL
{
    public class WscapesDBContext:DbContext
    {
        public WscapesDBContext() : base("WscapesDBConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("walid");

            WordInfo.SetEntityConfiguration(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WordInfo> WordsInfo { get; set; }
    }
}
