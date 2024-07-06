// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using AlbBlogger1.Data;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
//                        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//
// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();
// builder.Services.AddControllersWithViews();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseMigrationsEndPoint();
// }
// else
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }
//
// app.UseHttpsRedirection();
// app.UseStaticFiles();
//
// app.UseRouting();
//
// app.UseAuthorization();
//
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
// app.MapRazorPages();
//
// app.Run();

using AlbBlogger1.Areas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AlbBlogger1.Data;
using AlbBlogger1.Repositories;
using AlbBlogger1.Services;
using NestAlbania.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();




builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>>();
#region Scoped
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<RoleRepository, RoleRepository>();
#endregion

#region Transient
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IFileHandlerService, FileHandlerService>();
#endregion


// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseMigrationsEndPoint();
// }
// else
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }
//
// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseAuthorization();
//
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Account}/{action=Register}/{id?}");
// app.MapRazorPages();
//
// app.Run();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();