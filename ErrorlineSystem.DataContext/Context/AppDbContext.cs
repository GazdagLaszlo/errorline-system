using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorlineSystem.DataContext.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentOrder> EquipmentOrders { get; set; }
    }
}
