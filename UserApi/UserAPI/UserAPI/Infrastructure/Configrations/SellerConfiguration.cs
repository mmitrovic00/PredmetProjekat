using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
{
	public class SellerConfiguration : IEntityTypeConfiguration<Seller>
	{
		public void Configure(EntityTypeBuilder<Seller> builder)
		{
			
		}
	}
}
