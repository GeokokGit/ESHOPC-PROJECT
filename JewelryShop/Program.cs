using Microsoft.EntityFrameworkCore;
using JewelryShop.Data;
using JewelryShop.Components;
using JewelryShop.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<JewelryShop.Services.CartService>();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddSingleton<AdminSession>();

builder.Services.AddScoped<CartService>();



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(db);
}



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();