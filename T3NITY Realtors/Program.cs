using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using T3NITY_Realtors.Entities;
using T3NITY_Realtors.Repository;
using T3NITY_Realtors.Repository.IRepository;
using T3NITY_Realtors.Services;
using T3NITY_Realtors.Services.IServices;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddTransient<ICustomerServices,CustomerServices>();
builder.Services.AddTransient<ILandlordServices,LandlordServices>();
builder.Services.AddTransient<IUserServices,UserServices>();
builder.Services.AddTransient<IAdminServices,AdminServices>();
builder.Services.AddTransient<IDbOperations,DbOperations>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RealtorDb")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(5);//You can set Time   
});
builder.Services.AddMvc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
