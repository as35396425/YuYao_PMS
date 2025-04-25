using MvcProgram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
var connectString =builder.Configuration.GetConnectionString("ProductContext"); 
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProductContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("ProductContext")));

builder.Services.AddDbContext<IdentityContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("ProductContext")));

builder.Services.Configure<Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionOptions>(options =>
{
    options.HttpsPort = 5001; // 替換為你的實際HTTPS端口
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option=> {
        option.LoginPath = new PathString("/user2/login") ; 
        option.AccessDeniedPath =new PathString ("/user2/login"); // 可選：沒有權限時導向

    });
builder.Services.AddDefaultIdentity<applicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<IdentityContext>();
                
                



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
