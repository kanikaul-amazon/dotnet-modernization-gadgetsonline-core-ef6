using GadgetsOnline.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Npgsql;

namespace GadgetsOnline.Models
{
    public class GadgetsOnlinePostgreSqlConfiguration : DbConfiguration
    {
        public GadgetsOnlinePostgreSqlConfiguration()
        {
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
        }
    }

    [DbConfigurationType(typeof(GadgetsOnlinePostgreSqlConfiguration))]
    public class GadgetsOnlineEntities : DbContext
    {
        // Default constructor using connection string name from config
        public GadgetsOnlineEntities() : base("name=GadgetsOnlineEntities")
        {
            // Enable lazy loading by default (alternative to AutoInclude)
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        // Constructor with explicit connection string
        public GadgetsOnlineEntities(string dbConn) : base(dbConn)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL is case-sensitive, so we need to ensure tables and schemas are properly configured
            modelBuilder.HasDefaultSchema("atx-database-rds_dbo"); // Set default schema from mapping
            
            // Configure entity tables with PostgreSQL naming
            modelBuilder.Entity<Product>().ToTable("products", "atx-database-rds_dbo");
            modelBuilder.Entity<Category>().ToTable("categories", "atx-database-rds_dbo");
            modelBuilder.Entity<Cart>().ToTable("carts", "atx-database-rds_dbo");
            modelBuilder.Entity<Order>().ToTable("orders", "atx-database-rds_dbo");
            modelBuilder.Entity<OrderDetail>().ToTable("orderdetails", "atx-database-rds_dbo");

            // Configure relationships
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithRequired(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Cart>()
                .HasRequired(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithRequired(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasRequired(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);
                
            // PostgreSQL DateTimes should use UTC to avoid time zone issues
            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("timestamp with time zone"));
        }

    }


}

