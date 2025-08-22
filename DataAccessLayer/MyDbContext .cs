using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DataAccessLayer
{
    //Install: Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Design
    // dotnet ef migrations add Initial
    // dotnet ef database update
    public class MyDbContext : DbContext
    {
        private readonly string _windowsConnectionString = @"Server=.\SQLExpress;Database=Lab5Database1;Trusted_Connection=True;TrustServerCertificate=true";
        //private readonly string _windowsConnectionString = @"Server=localhost\SQLEXPRESS;Database=Lab5Database1;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<ProjectDeveloper> ProjectDevelopers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Address> Addresses { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_windowsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //one-to-one
            builder.Entity<User>()
                .HasOne(f => f.Type)
                .WithMany(c => c.Users)
                .HasForeignKey(f => f.TypeId);
            //one-to-one 
            builder.Entity<Warehouse>()
                .HasOne(w => w.Address)
                .WithOne(a => a.Warehouse)
                .HasForeignKey<Address>(a => a.WarehouseId);
            //one-to-many
            builder.Entity<Company>()
               .HasMany(c => c.Employees)
               .WithOne(e => e.Company)
               .HasForeignKey(e => e.CompanyId);

            //many-to-many
            builder.Entity<ProjectDeveloper>()
                .HasKey(pd => new { pd.ProjectId, pd.DeveloperId });

            builder.Entity<ProjectDeveloper>()
                .HasOne(pd => pd.Project)
                .WithMany(p => p.ProjectDevelopers)
                .HasForeignKey(pd => pd.ProjectId);

            builder.Entity<ProjectDeveloper>()
                .HasOne(pd => pd.Developer)
                .WithMany(d => d.ProjectDevelopers)
                .HasForeignKey(pd => pd.DeveloperId);

            Seed(builder);

        }
        protected void Seed(ModelBuilder modelBuilder)
        {
            // Many-to-Many: Developer - Project
            var dev1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var dev2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var proj1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var proj2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");

            modelBuilder.Entity<Developer>().HasData(
                new Developer { Id = dev1Id, Name = "Ana Popescu" },
                new Developer { Id = dev2Id, Name = "Ion Georgescu" }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = proj1Id, Title = "E-Commerce Platform" },
                new Project { Id = proj2Id, Title = "Mobile App Development" }
            );

            modelBuilder.Entity<ProjectDeveloper>().HasData(
                new ProjectDeveloper { ProjectId = proj1Id, DeveloperId = dev1Id },
                new ProjectDeveloper { ProjectId = proj1Id, DeveloperId = dev2Id },
                new ProjectDeveloper { ProjectId = proj2Id, DeveloperId = dev2Id }
            );

            // One-to-Many: Company - Employees
            var company1Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var company2Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var emp1Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var emp2Id = Guid.Parse("88888888-8888-8888-8888-888888888888");

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = company1Id, Name = "Amazon" },
                new Company { Id = company2Id, Name = "Centric" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = emp1Id, FullName = "Maria Ionescu", CompanyId = company1Id },
                new Employee { Id = emp2Id, FullName = "George Vasilescu", CompanyId = company2Id }
            );

            // One-to-One: Warehouse - Address
            var warehouse1Id = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var address1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { Id = warehouse1Id, Name = "Central Warehouse" }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = address1Id,
                    Street = "Strada Fabricii 10",
                    City = "Cluj-Napoca",
                    WarehouseId = warehouse1Id
                }
            );
        }


    }
}
