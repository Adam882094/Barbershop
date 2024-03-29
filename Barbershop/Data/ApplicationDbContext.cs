﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Barbershop.Models;

namespace Barbershop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Appointment> Haircuts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Barbershop.Models.Haircut>? Haircut { get; set; }
    }
}