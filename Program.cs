using Microsoft.EntityFrameworkCore;
using OnlineShoppingStore.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with SQL Server connection string
builder.Services.AddDbContext<SafainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support with specific settings
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout setting
    options.Cookie.HttpOnly = true; // Make the cookie HttpOnly for security
    options.Cookie.IsEssential = true; // Mark the session cookie as essential for GDPR compliance
});

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Ensure session middleware is applied before routing
app.UseSession();  // <-- Session must be added before routing

// Middleware to handle HTTPS redirection (can be skipped in development)
app.UseHttpsRedirection();

// Set up routing and authorization
app.UseRouting();
app.UseAuthorization();

// Serve static files (like images, styles, etc.)
app.UseStaticFiles();

// Map default route for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();
