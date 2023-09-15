using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure
{
	public class WSDBContext : DbContext
	{
        //Ovde definisemo DbSetove (tabele)
        public DbSet<User> Users { get; set; }
      
        public DbSet<UserImage> UserImages { get; set; }
        public WSDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WSDBContext).Assembly);
        }
    }
}
