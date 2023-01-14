using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@32302e342e30EEanITQqMG1UNBfCCmUCSwJLCsWR9 VbXDarK21goyU=;Mgo DSMBaFt/QHRqVVhjVFpFaV1DQmFJfFBmQmlbeVRzdEU3HVdTRHRcQlxiTX5UdENgWXhbcXE=;Mgo DSMBMAY9C3t2VVhkQlFadVdJXHxLf0x0RWFab1l6dlNMY1xBNQtUQF1hSn5Sd0FjXn1Yc3RSQ2NZ;Mgo DSMBPh8sVXJ0S0J XE9HflRDQmFIYVF2R2BJeFRycl9FYEwgOX1dQl9gSX9SckVkW3ldcHNQQ2U=;@32302e342e30jAqo8FA5RHy4fYei3P9q1E yndwEn37Rxnd9bj5R3rg=;NRAiBiAaIQQuGjN/V0Z WE9EaFxKVmJWfFJpR2NbfE53fldAal9RVAciSV9jS31Td0RnWXhcdndVQWVUVA==;NT8mJyc2IWhhY31nfWN9YGtoYnxhYnxhY2Fgc2RpYWVpYGNzEh5oMDw/Nn0kNiE9NiETND4yOj99MDw ;ORg4AjUWIQA/Gnt2VVhkQlFadVdJXHxLf0x0RWFab1l6dlNMY1xBNQtUQF1hSn5Sd0FjXn1Yc3RSQGJZ;@32302e342e30LRCvg4mUPrdIXepOOR9HC oQpNl2dXLaJ935n278Cjw=;@32302e342e30aQs7Duisn18y9gp1piUCCjJRmCE2r/Kv/t53usm0/SM=;@32302e342e30Zdxylw7FotO16X4JulIWlEC0GYqwQoW2YNttUiX gK4=;@32302e342e30m3l2h/ss9Ng52Fms86IqI/pDMUTdIGCHwRmEqwDi nY=;@32302e342e30EEanITQqMG1UNBfCCmUCSwJLCsWR9 VbXDarK21goyU=");

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
