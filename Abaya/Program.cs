using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System;
using Abaya.Models;

var builder = WebApplication.CreateBuilder(args);

#region Services

// MVC
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection")
    )
);

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.AccessDeniedPath = "/User/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(28);
    options.Cookie.HttpOnly = true;
});

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(14);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.MaxAge = TimeSpan.FromDays(14);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

#endregion

var app = builder.Build();

#region Middleware

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

#endregion

#region Routes

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}"
);

#endregion

app.Run();
