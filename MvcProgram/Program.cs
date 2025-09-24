using MvcProgram.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
var connectString =builder.Configuration.GetConnectionString("ProductContext"); 
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductService, ProductService>();

// builder.Services.AddDbContext<ProductContext>(options =>
// options.UseSqlite(builder.Configuration.GetConnectionString("ProductContext")));

builder.Services.AddDbContext<IdentityContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("ProductContext")));

builder.Configuration.AddJsonFile(
    "Properties/appsettings.json",
    optional: true,
    reloadOnChange: true
    );

var JWT_Token = builder.Configuration.GetSection("JWT");


// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(option=> {
//         option.LoginPath = new PathString("/user/login") ; 
//         option.AccessDeniedPath =new PathString ("/user/login"); // 可選：沒有權限時導向( 有權限 但不夠大)

//     });
builder.Services.AddDefaultIdentity<applicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
.AddEntityFrameworkStores<IdentityContext>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        //Challenge 是甚麼東西
        //為甚麼要設定authenticateScheme
    }

).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,    
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_Token["Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("jwt"))
            {
                context.Token = context.Request.Cookies["jwt"];
            }
            return Task.CompletedTask;
        }
    };
});




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
