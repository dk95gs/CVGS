using System;
using System.Collections.Generic;
using System.Text;
using CVGS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CVGS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Games> Games { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<EventRegistration> EventRegistration { get; set; }
        public DbSet<CreditCards> CreditCards { get; set; }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<WishLists> WishLists { get; set; }
        public DbSet<FriendsFamilyLists> FriendsFamilyLists { get; set; }
        public DbSet<FriendsAwaitingApproval> FriendsAwaitingApprovals { get; set; }
        public DbSet<ShippingMailingAddresses> ShippingMailingAddresses { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OwnedGames> OwnedGames { get; set; }
        public DbSet<Shipments> Shipments { get; set; }
    }
}
