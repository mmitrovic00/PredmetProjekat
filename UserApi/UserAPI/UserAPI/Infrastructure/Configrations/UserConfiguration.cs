using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Email); //Podesavam primarni kljuc tabele


			builder.Property(x => x.FirstName).HasMaxLength(30);//kazem da je maks duzina 30 karaktera
			builder.Property(x => x.LastName).HasMaxLength(30);

			builder.HasIndex(x => x.UserName).IsUnique();
		}
	}
}
