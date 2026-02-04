using JewelryShop.Models;

namespace JewelryShop.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if (db.Products.Any())
            return;

        db.Products.AddRange(
            new Product
            {
                Name = "Golden Ring",
                Description = "",
                Price = 100m,
                ImageUrl = "images/ring 1.jpg",
                Rating = 4,
                Quantity = 10
            },
            new Product
            {
                Name = "Silver Bracelet",
                Description = "",
                Price = 150m,
                ImageUrl = "images/bracelet1.jpg",
                Rating = 4,
                Quantity = 10
            },
            new Product
            {
                Name = "Silver Leaf Earrings",
                Description = "Sterling Silver Ivy Vine Art Nouveau Earrings",
                Price = 95m,
                ImageUrl = "images/earring1.jpg",
                Rating = 4,
                Quantity = 5
            },
            new Product
            {
                Name = "Rose Gold Necklace",
                Description = "",
                Price = 75m,
                ImageUrl = "images/necklace 1 (2).jpg",
                Rating = 4,
                Quantity = 10
            },
            new Product
            {
                Name = "Metal Dragon Bracelet",
                Description = "",
                Price = 35m,
                ImageUrl = "images/dragon bracelet.jpg",
                Rating = 5,
                Quantity = 10
            }
        );

        db.SaveChanges();
    }
}

