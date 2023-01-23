using IntegralTradingJSSignalREfcore.Hubs;
using IntegralTradingJSSignalREfcore.Models;
using IntegralTradingJSSignalREfcore.Repository;
using IntegralTradingJSSignalREfcore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<DevExtremeContext>(X => X.UseSqlServer(builder.Configuration.GetConnectionString("sqlString")));

builder.Services.AddSignalR();

builder.Services.AddScoped<IHviService, HviService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<HviHub>("/hvihub");

app.Run();
