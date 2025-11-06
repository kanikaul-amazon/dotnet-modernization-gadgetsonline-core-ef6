using Npgsql;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GadgetsOnline.Models
{
    public class GadgetsOnlineInitializer : CreateDatabaseIfNotExists<GadgetsOnlineEntities>
    {
        protected override void Seed(GadgetsOnlineEntities context)
        {
            // Create sequences for identity columns if they don't exist (PostgreSQL requirement)
            EnsureSequencesExist(context);

            // Categories
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Mobile Phones", Description = "Latest collection of Mobile Phones" },
                new Category { CategoryId = 2, Name = "Laptops", Description = "Latest Laptops in 2022" },
                new Category { CategoryId = 3, Name = "Desktops", Description = "Latest Desktops in 2022" },
                new Category { CategoryId = 4, Name = "Audio", Description = "Latest audio devices" },
                new Category { CategoryId = 5, Name = "Accessories", Description = "USB Cables, Mobile chargers and Keyboards etc" }
            };
            categories.ForEach(c => context.Categories.Add(c));

            // Products
            var products = new List<Product>
            {
                new Product { ProductId = 1, CategoryId = 1, Name = "Phone 12", Price = 699.00M, ProductArtUrl = "/Content/Images/Mobile/1.jpg" },
                new Product { ProductId = 2, CategoryId = 1, Name = "Phone 13 Pro", Price = 999.00M, ProductArtUrl = "/Content/Images/Mobile/2.jpg" },
                new Product{ ProductId = 3, CategoryId=1, Name="Phone 13 Pro Max", Price=1199.00M, ProductArtUrl = "/Content/Images/Mobile/3.jpg"},
                new Product{ ProductId = 4, CategoryId=2, Name="XTS 13'", Price=899.00M, ProductArtUrl = "/Content/Images/Laptop/1.jpg"},
                new Product{ ProductId = 5, CategoryId=2, Name="PC 15.5'", Price=479.00M, ProductArtUrl = "/Content/Images/Laptop/2.jpg"},
                new Product{ ProductId = 6, CategoryId=2, Name="Notebook 14", Price=169.00M, ProductArtUrl = "/Content/Images/Laptop/3.jpg"},
                new Product{ ProductId = 7, CategoryId=3, Name="The IdeaCenter", Price=539.00M, ProductArtUrl = "/Content/Images/placeholder.gif"},
                new Product{ ProductId = 8, CategoryId=3, Name="COMP 22-df003w", Price=389.00M, ProductArtUrl = "/Content/Images/placeholder.gif"},
                new Product{ ProductId = 9, CategoryId=4, Name="Bluetooth Headphones Over Ear", Price=28.00M, ProductArtUrl = "/Content/Images/Headphones/1.png"},
                new Product{ ProductId = 10, CategoryId=4, Name="ZX Series ", Price=10.00M, ProductArtUrl = "/Content/Images/Headphones/2.png"},
                new Product{ ProductId = 11, CategoryId=5, Name="Wireless charger", Price=9.99M, ProductArtUrl = "/Content/Images/placeholder.gif"},
                new Product{ ProductId = 12, CategoryId=5, Name="Mousepad", Price=2.99M, ProductArtUrl = "/Content/Images/placeholder.gif"},
                new Product{ ProductId = 13, CategoryId=5, Name="Keyboard", Price=9.99M, ProductArtUrl = "/Content/Images/placeholder.gif"},
            };
            products.ForEach(p => context.Products.Add(p));

            context.SaveChanges();
            
            // Update sequence values after inserting data with explicit IDs (PostgreSQL specific)
            UpdateSequenceValues(context);
        }
        
        private void EnsureSequencesExist(GadgetsOnlineEntities context)
        {
            // Execute raw SQL to create sequences if they don't exist
            // These are PostgreSQL-specific operations for identity columns
            var commands = new[]
            {
                "CREATE SEQUENCE IF NOT EXISTS public.""Categories_CategoryId_seq"" START WITH 6",
                "CREATE SEQUENCE IF NOT EXISTS public.""Products_ProductId_seq"" START WITH 14",
                "CREATE SEQUENCE IF NOT EXISTS public.""Carts_RecordId_seq""",
                "CREATE SEQUENCE IF NOT EXISTS public.""Orders_OrderId_seq""",
                "CREATE SEQUENCE IF NOT EXISTS public.""OrderDetails_OrderDetailId_seq"""
            };

            foreach (var cmd in commands)
            {
                context.Database.ExecuteSqlCommand(cmd);
            }
        }

        private void UpdateSequenceValues(GadgetsOnlineEntities context)
        {
            // PostgreSQL requires manually updating sequences after inserting records with explicit IDs
            var commands = new[]
            {
                "SELECT SETVAL('public.""Categories_CategoryId_seq""', (SELECT MAX(""CategoryId"") FROM public.""Categories""))",
                "SELECT SETVAL('public.""Products_ProductId_seq""', (SELECT MAX(""ProductId"") FROM public.""Products""))"
            };

            foreach (var cmd in commands)
            {
                context.Database.ExecuteSqlCommand(cmd);
            }
        }
    }
}
