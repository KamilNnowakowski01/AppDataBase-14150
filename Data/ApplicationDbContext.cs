using AppModel.Models; // Ensure this namespace matches your model's namespace
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppDataBase.Data // Change to your actual namespace
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<ComputerProcessor> ComputerProcessors { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Producer> Producers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relations
            // Many-to-Many: Book and Author
            modelBuilder.Entity<ComputerProcessor>()
                .HasKey(ba => new { ba.ComputerId, ba.ProcessorId });

            modelBuilder.Entity<ComputerProcessor>()
                .HasOne(ba => ba.Computer)
                .WithMany(b => b.ComputerProcessors)
                .HasForeignKey(ba => ba.ComputerId);

            modelBuilder.Entity<ComputerProcessor>()
                .HasOne(ba => ba.Computer)
                .WithMany(a => a.ComputerProcessors)
                .HasForeignKey(ba => ba.ProcessorId);
            // One-to-Many: Book and Publisher
            modelBuilder.Entity<Computer>()
                .HasOne(b => b.Producer)
                .WithMany(p => p.Computers)
                .HasForeignKey(b => b.ProducerId);

            // Seed initial data (example)
            modelBuilder.Entity<Computer>().HasData(
                new Computer { ComputerId = 1, ComputerName = "Gamer Extreme", MemoryRam = 16, MemoryDisk = 512, GraphicsCard = "NVIDIA RTX 3070", DateOfProduction = new DateTime(2023, 1, 10), ProducerId = 1 },
                new Computer { ComputerId = 2, ComputerName = "Office Pro", MemoryRam = 8, MemoryDisk = 256, GraphicsCard = "Intel Integrated", DateOfProduction = new DateTime(2022, 8, 24), ProducerId = 2 },
                new Computer { ComputerId = 3, ComputerName = "Workstation Power", MemoryRam = 32, MemoryDisk = 1024, GraphicsCard = "AMD Radeon RX 6800", DateOfProduction = new DateTime(2023, 2, 15), ProducerId = 3 },
                new Computer { ComputerId = 4, ComputerName = "Ultra Slim", MemoryRam = 16, MemoryDisk = 512, GraphicsCard = "NVIDIA GTX 1650", DateOfProduction = new DateTime(2023, 1, 20), ProducerId = 4 },
                new Computer { ComputerId = 5, ComputerName = "Graphic Designer Pro", MemoryRam = 64, MemoryDisk = 2048, GraphicsCard = "NVIDIA RTX 3090", DateOfProduction = new DateTime(2023, 3, 5), ProducerId = 1 },
                new Computer { ComputerId = 6, ComputerName = "Budget Friendly", MemoryRam = 4, MemoryDisk = 128, GraphicsCard = "Intel Integrated", DateOfProduction = new DateTime(2023, 2, 28), ProducerId = 2 }

            );

            modelBuilder.Entity<Processor>().HasData(
                new Processor { ProcessorId = 1, Name = "Intel Core i7-10700K", Series = "Core i7", Socket = "LGA1200", CoreClockGHz = 3.8f, NumberofCores = 8 },
                new Processor { ProcessorId = 2, Name = "AMD Ryzen 9 5900X", Series = "Ryzen 9", Socket = "AM4", CoreClockGHz = 3.7f, NumberofCores = 12 },
                new Processor { ProcessorId = 3, Name = "Intel Core i5-11600K", Series = "Core i5", Socket = "LGA1200", CoreClockGHz = 3.9f, NumberofCores = 6 }

            );

            modelBuilder.Entity<ComputerProcessor>().HasData(
                new ComputerProcessor { Id = 1, ComputerId = 1, ProcessorId = 2 },
                new ComputerProcessor { Id = 2, ComputerId = 2, ProcessorId = 1 },
                new ComputerProcessor { Id = 3, ComputerId = 3, ProcessorId = 3 },
                new ComputerProcessor { Id = 4, ComputerId = 4, ProcessorId = 2 },
                new ComputerProcessor { Id = 5, ComputerId = 5, ProcessorId = 1 },
                new ComputerProcessor { Id = 6, ComputerId = 6, ProcessorId = 3 }
            );

            modelBuilder.Entity<Producer>().HasData(
                new Producer { ProducerId = 1, Name = "Tech Innovations Inc.", Address = "123 Tech Drive, Silicon Valley, CA", YearFounded = 1984 },
                new Producer { ProducerId = 2, Name = "Global Computing Ltd.", Address = "456 Global Way, London, UK", YearFounded = 1978 },
                new Producer { ProducerId = 3, Name = "Creative Solutions Inc.", Address = "789 Innovation Parkway, Boston, MA", YearFounded = 1992 },
                new Producer { ProducerId = 4, Name = "Portable Tech Co.", Address = "101 Mobile Avenue, Tokyo, Japan", YearFounded = 1986 }

            );
        }
    }
}
