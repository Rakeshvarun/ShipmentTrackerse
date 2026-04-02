using Microsoft.EntityFrameworkCore;
using ShipmentTrackers.Models;
using System.Collections.Generic;

namespace ShipmentTrackers.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Shipment> Shipments { get; set; }
    }
}
