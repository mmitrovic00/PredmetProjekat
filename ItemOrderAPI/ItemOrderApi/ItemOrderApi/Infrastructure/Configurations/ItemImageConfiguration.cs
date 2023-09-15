using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
{
	public class ItemImageConfiguration : IEntityTypeConfiguration<ItemImage>
    {
        public void Configure(EntityTypeBuilder<ItemImage> builder)
        {
            builder.HasKey(x => x.ItemImageId);
            builder.Property(x => x.ItemImageId).ValueGeneratedOnAdd();
        }
    }
}
