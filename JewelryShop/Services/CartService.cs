using JewelryShop.Data;
using JewelryShop.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryShop.Services;

public class CartLine
{
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class CartService
{
    private readonly AppDbContext _db;

    public List<CartLine> Lines { get; } = new();

    public CartService(AppDbContext db)
    {
        _db = db;
    }

    public decimal Subtotal() => Lines.Sum(l => l.Price * l.Quantity);
    public decimal Tax() => Math.Round(Subtotal() * 0.15m, 2);  
    public decimal Total() => Subtotal() + Tax();

    public async Task<(bool ok, string msg)> AddAsync(int productId, int qty = 1)
    {
        if (qty <= 0) qty = 1;

        var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == productId);
        if (p == null) return (false, "Product not found.");

        if (p.Quantity < qty)
            return (false, $"Not enough stock. Available: {p.Quantity}");

        
        p.Quantity -= qty;
        await _db.SaveChangesAsync();

       
        var line = Lines.FirstOrDefault(l => l.ProductId == productId);
        if (line == null)
        {
            Lines.Add(new CartLine
            {
                ProductId = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Quantity = qty
            });
        }
        else
        {
            line.Quantity += qty;
        }

        return (true, "Added to cart.");
    }

    public async Task RemoveLineAsync(int productId)
    {
        var line = Lines.FirstOrDefault(l => l.ProductId == productId);
        if (line == null) return;

       
        var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == productId);
        if (p != null)
        {
            p.Quantity += line.Quantity;
            await _db.SaveChangesAsync();
        }

        Lines.Remove(line);
    }

    public async Task<(bool ok, string msg)> SetQuantityAsync(int productId, int newQty)
    {
        if (newQty < 1) newQty = 1;

        var line = Lines.FirstOrDefault(l => l.ProductId == productId);
        if (line == null) return (false, "Line not found.");

        var p = await _db.Products.FirstOrDefaultAsync(x => x.Id == productId);
        if (p == null) return (false, "Product not found.");

        var diff = newQty - line.Quantity;

        if (diff > 0)
        {
           
            if (p.Quantity < diff)
                return (false, $"Not enough stock. Available: {p.Quantity}");

            p.Quantity -= diff;
            line.Quantity = newQty;
            await _db.SaveChangesAsync();
            return (true, "Updated.");
        }
        else if (diff < 0)
        {
            
            p.Quantity += (-diff);
            line.Quantity = newQty;
            await _db.SaveChangesAsync();
            return (true, "Updated.");
        }

        return (true, "No changes.");
    }

    public async Task ClearAsync()
    {
        
        foreach (var line in Lines.ToList())
            await RemoveLineAsync(line.ProductId);
    }
}
