
using GetGroup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class UserServiceConfiguration : IEntityTypeConfiguration<UserService>
    {
        public void Configure(EntityTypeBuilder<UserService> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.RequestSatusId).IsRequired();
            builder.Property(p => p.ServiceId).IsRequired();
            builder.Property(p => p.UserId).IsRequired();

            builder.HasOne(p => p.AppUser).WithMany(x=>x.UserServices)
                .HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Service).WithMany(x => x.UserServices)
                .HasForeignKey(p => p.ServiceId);
        }
    }
}
