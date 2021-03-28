using GetGroup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GetGroup.Infrastructure.Data
{
    public class GetGroupContext : DbContext
    {
        public GetGroupContext(DbContextOptions<GetGroupContext> options) : base(options)
        {
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserService> UserServices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
