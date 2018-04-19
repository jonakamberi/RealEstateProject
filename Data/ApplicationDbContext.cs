﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateProject.Models;

namespace RealEstateProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Property>().ToTable("Properties");
        }

        public DbSet<RealEstateProject.Models.Property> Property { get; set; }
        public DbSet<RealEstateProject.Models.PropertyType> PropertyType { get; set; }
        public DbSet<RealEstateProject.Models.Penthouse> Penthouse { get; set; }
    }
}
