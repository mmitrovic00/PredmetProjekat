using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId); //Podesavam primarni kljuc tabele

            builder.Property(x => x.OrderId).ValueGeneratedOnAdd(); //Kazem da ce se primarni kljuc
                                                               //automatski generisati prilikom dodavanja,
                                                               //redom 1 2 3...

            

        }

    }
}
