using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.Models;

namespace WebProjekat.Infrastructure.Configurations
{
	public class UserImageConfiguration : IEntityTypeConfiguration<UserImage>
    {
        public void Configure(EntityTypeBuilder<UserImage> builder)
        {
            builder.HasKey(x => x.UserImageID);
            builder.Property(x => x.UserImageID).ValueGeneratedOnAdd();
        }
	}
}
