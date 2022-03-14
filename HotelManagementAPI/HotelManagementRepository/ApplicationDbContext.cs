
using HotelManagementCommon.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManagementRepository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
         : base(options)
        {

        }
        public virtual DbSet<RoomsEntity> Rooms { get; set; }
        public virtual DbSet<CustomerDetailsEntity> CustomerDetails { get; set; }
        public virtual DbSet<BookingDetailsEntity> BookingDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
 
    }
