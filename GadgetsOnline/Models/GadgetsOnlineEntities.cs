using GadgetsOnline.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Npgsql;

namespace GadgetsOnline.Models
{
    public class GadgetsOnlineEntitiesPostgreSqlConfiguration : DbConfiguration
    {
        public GadgetsOnlineEntitiesPostgreSqlConfiguration()
        {
            SetProviderServices("Npgsql", Npgsql.NpgsqlServices.Instance);
            SetDefaultConnectionFactory(new Npgsql.NpgsqlConnectionFactory());
        }
    }
    
    [DbConfigurationType(typeof(GadgetsOnlineEntitiesPostgreSqlConfiguration))]
    public class GadgetsOnlineEntities : DbContext
    {
        // Default constructor using connection string name from config
        public GadgetsOnlineEntities() : base("name=GadgetsOnlineEntities")
        {
            // Enable lazy loading by default (alternative to AutoInclude)
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            
            // Configure Npgsql date/time handling for PostgreSQL compatibility
            var connectionFactory = this.Database.Connection.GetType();
            if (connectionFactory.FullName.Contains("Npgsql"))
            {
                // Force DateTime values to be sent as UTC
                this.Database.CommandTimeout = 90; // Increase command timeout for PostgreSQL operations
            }
        }

        // Constructor with explicit connection string
        public GadgetsOnlineEntities(string dbConn) : base(dbConn)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            
            // Configure Npgsql date/time handling for PostgreSQL compatibility
            var connectionFactory = this.Database.Connection.GetType();
            if (connectionFactory.FullName.Contains("Npgsql"))
            {
                // Force DateTime values to be sent as UTC
                this.Database.CommandTimeout = 90; // Increase command timeout for PostgreSQL operations
            }
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Set default schema for PostgreSQL
            modelBuilder.HasDefaultSchema("public");
            
            // Disable PluralizingTableNameConvention to match PostgreSQL naming conventions
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
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
            
            // Configure decimal precision for PostgreSQL compatibility
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(19, 4);
                
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasPrecision(19, 4);
                
            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasPrecision(19, 4);
        }

    }


}

