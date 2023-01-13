using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NorthwindDbContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("NorthwindDb"));
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
	app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}"
    );
});

if (app.Environment.IsDevelopment())
{
    app.UseSpa(spa =>
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:5173");
    });
}
else
{
    app.MapFallbackToFile("index.html");
}

app.Run();
