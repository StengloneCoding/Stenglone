namespace StenglonesApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using StenglonesApi.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MachineMetric> MachineMetrics { get; set; }
    }

}