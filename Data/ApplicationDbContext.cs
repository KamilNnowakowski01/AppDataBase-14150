using AppModel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppDataBase.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<Graphics> Graphics { get; set; }
        public DbSet<ComputersGraphics> ComputersGraphics { get; set; }
        public DbSet<Producer> Producers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComputersGraphics>()
                .HasKey(ba => new { ba.ComputerId, ba.GraphicsId });

            modelBuilder.Entity<ComputersGraphics>()
                .HasOne(ba => ba.Computer)
                .WithMany(b => b.ComputersGraphics)
                .HasForeignKey(ba => ba.ComputerId);

            modelBuilder.Entity<ComputersGraphics>()
                .HasOne(ba => ba.Graphics)
                .WithMany(a => a.ComputersGraphics)
                .HasForeignKey(ba => ba.GraphicsId);

            modelBuilder.Entity<Computer>()
                .HasOne(b => b.Producer)
                .WithMany(p => p.Computers)
                .HasForeignKey(b => b.ProducerId);

            // Seed initial data (example)

            modelBuilder.Entity<Graphics>().HasData(
                new Graphics() { GraphicsId = 1, Name = "NVIDIA RTX 3080", MemoryGB = 10, ConnectorType = "PCIe 4.0 x16", CoreClockMHz = 1440, RecommendedPower = 320 },
                new Graphics() { GraphicsId = 2, Name = "AMD Radeon RX 6800", MemoryGB = 16, ConnectorType = "PCIe 4.0 x16", CoreClockMHz = 1815, RecommendedPower = 250 },
                new Graphics() { GraphicsId = 3, Name = "NVIDIA GTX 1660 Ti", MemoryGB = 6, ConnectorType = "PCIe 3.0 x16", CoreClockMHz = 1500, RecommendedPower = 120 },
                new Graphics() { GraphicsId = 4, Name = "AMD Radeon RX 5500 XT", MemoryGB = 8, ConnectorType = "PCIe 4.0 x8", CoreClockMHz = 1717, RecommendedPower = 130 },
                new Graphics() { GraphicsId = 5, Name = "NVIDIA RTX 2060 Super", MemoryGB = 8, ConnectorType = "PCIe 3.0 x16", CoreClockMHz = 1470, RecommendedPower = 175 },
                new Graphics() { GraphicsId = 6, Name = "AMD Radeon RX 5700", MemoryGB = 8, ConnectorType = "PCIe 4.0 x16", CoreClockMHz = 1465, RecommendedPower = 180 },
                new Graphics() { GraphicsId = 7, Name = "NVIDIA RTX 3090", MemoryGB = 24, ConnectorType = "PCIe 4.0 x16", CoreClockMHz = 1395, RecommendedPower = 350 }
            );

            modelBuilder.Entity<Producer>().HasData(
                new Producer() { ProducerId = 1, Name = "Tech Innovations Inc.", Address = "123 Tech Drive, Silicon Valley, CA", YearFounded = 1984 },
                new Producer() { ProducerId = 2, Name = "Global Computing Ltd.", Address = "456 Global Way, London, UK", YearFounded = 1978 },
                new Producer() { ProducerId = 3, Name = "Creative Solutions Inc.", Address = "789 Innovation Parkway, Boston, MA", YearFounded = 1992 },
                new Producer() { ProducerId = 4, Name = "Portable Tech Co.", Address = "101 Mobile Avenue, Tokyo, Japan", YearFounded = 1986 }
            );

            modelBuilder.Entity<Computer>().HasData(
                new Computer() { ComputerId = 1, Name = "Gamer Extreme", MemoryRam = 16, MemoryDisk = 512, Processor = "Intel Core i7-10700K", DateOfProduction = new DateTime(2023, 1, 10), ProducerId = 1 },
                new Computer() { ComputerId = 2, Name = "Office Pro", MemoryRam = 8, MemoryDisk = 256, Processor = "Intel Core i7-10700K", DateOfProduction = new DateTime(2022, 8, 24), ProducerId = 2 },
                new Computer() { ComputerId = 3, Name = "Workstation Power", MemoryRam = 32, MemoryDisk = 1024, Processor = "AMD Ryzen 9 5900X", DateOfProduction = new DateTime(2023, 2, 15), ProducerId = 3 },
                new Computer() { ComputerId = 4, Name = "Ultra Slim", MemoryRam = 16, MemoryDisk = 512, Processor = "Intel Core i5-11600K", DateOfProduction = new DateTime(2023, 1, 20), ProducerId = 4 },
                new Computer() { ComputerId = 5, Name = "Graphic Designer Pro", MemoryRam = 64, MemoryDisk = 2048, Processor = "Intel Core i5-11600K", DateOfProduction = new DateTime(2023, 3, 5), ProducerId = 1 },
                new Computer() { ComputerId = 6, Name = "Budget Friendly", MemoryRam = 4, MemoryDisk = 128, Processor = "AMD Ryzen 9 5900X", DateOfProduction = new DateTime(2023, 2, 28), ProducerId = 2 }

            );

            modelBuilder.Entity<ComputersGraphics>().HasData(
                new ComputersGraphics() { Id = 1, ComputerId = 1, GraphicsId = 7 },
                new ComputersGraphics() { Id = 2, ComputerId = 2, GraphicsId = 3 },
                new ComputersGraphics() { Id = 3, ComputerId = 3, GraphicsId = 1 },
                new ComputersGraphics() { Id = 4, ComputerId = 4, GraphicsId = 5 },
                new ComputersGraphics() { Id = 5, ComputerId = 5, GraphicsId = 2 },
                new ComputersGraphics() { Id = 6, ComputerId = 6, GraphicsId = 4 },
                new ComputersGraphics() { Id = 7, ComputerId = 1, GraphicsId = 2 },
                new ComputersGraphics() { Id = 8, ComputerId = 3, GraphicsId = 6 },
                new ComputersGraphics() { Id = 9, ComputerId = 5, GraphicsId = 7 }
            );
        }
    }
}
