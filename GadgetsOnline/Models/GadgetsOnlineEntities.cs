using GadgetsOnline.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public override int SaveChanges()
        {
            FixDateTimeKinds();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FixDateTimeKinds();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void FixDateTimeKinds()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                foreach (var property in entry.CurrentValues.PropertyNames)
                {
                    var value = entry.CurrentValues[property];
                    if (value is DateTime dateTime && dateTime.Kind != DateTimeKind.Utc)
                    {
                        entry.CurrentValues[property] = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    }
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure table and schema mappings
            modelBuilder.Entity<Product>()
                .ToTable("products", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Product>().Property(p => p.ProductId).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnName("name");
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnName("price");
            modelBuilder.Entity<Product>().Property(p => p.ProductArtUrl).HasColumnName("productarturl");

            modelBuilder.Entity<Category>()
                .ToTable("categories", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Category>().Property(c => c.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("name");
            modelBuilder.Entity<Category>().Property(c => c.Description).HasColumnName("description");

            modelBuilder.Entity<Cart>()
                .ToTable("carts", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Cart>().Property(c => c.RecordId).HasColumnName("recordid");
            modelBuilder.Entity<Cart>().Property(c => c.CartId).HasColumnName("cartid");
            modelBuilder.Entity<Cart>().Property(c => c.ProductId).HasColumnName("productid");
            modelBuilder.Entity<Cart>().Property(c => c.Count).HasColumnName("count");
            modelBuilder.Entity<Cart>().Property(c => c.DateCreated).HasColumnName("datecreated");

            modelBuilder.Entity<Order>()
                .ToTable("orders", "bobsusedbookstore_dbo");
            modelBuilder.Entity<Order>().Property(o => o.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(o => o.OrderDate).HasColumnName("orderdate");
            modelBuilder.Entity<Order>().Property(o => o.Username).HasColumnName("username");
            modelBuilder.Entity<Order>().Property(o => o.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<Order>().Property(o => o.LastName).HasColumnName("lastname");
            modelBuilder.Entity<Order>().Property(o => o.Address).HasColumnName("address");
            modelBuilder.Entity<Order>().Property(o => o.City).HasColumnName("city");
            modelBuilder.Entity<Order>().Property(o => o.State).HasColumnName("state");
            modelBuilder.Entity<Order>().Property(o => o.PostalCode).HasColumnName("postalcode");
            modelBuilder.Entity<Order>().Property(o => o.Country).HasColumnName("country");
            modelBuilder.Entity<Order>().Property(o => o.Phone).HasColumnName("phone");
            modelBuilder.Entity<Order>().Property(o => o.Email).HasColumnName("email");
            modelBuilder.Entity<Order>().Property(o => o.Total).HasColumnName("total");

            modelBuilder.Entity<OrderDetail>()
                .ToTable("orderdetails", "bobsusedbookstore_dbo");
            modelBuilder.Entity<OrderDetail>().Property(od => od.OrderDetailId).HasColumnName("orderdetailid");
            modelBuilder.Entity<OrderDetail>().Property(od => od.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderDetail>().Property(od => od.ProductId).HasColumnName("productid");
            modelBuilder.Entity<OrderDetail>().Property(od => od.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<OrderDetail>().Property(od => od.UnitPrice).HasColumnName("unitprice");

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
        }

    }


}

