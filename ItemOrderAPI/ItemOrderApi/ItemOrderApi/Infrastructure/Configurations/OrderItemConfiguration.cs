using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.HasKey(x => x.OrderItemId);
            builder.HasOne(x => x.Item)
                   .WithMany(x => x.OrderItems)
                   .HasForeignKey(x => x.ItemId)
                   .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Order)
                       .WithMany(x => x.OrderedItems)
                       .HasForeignKey(x => x.OrderId)
                       .OnDelete(DeleteBehavior.NoAction);
        }
		
	}
}
