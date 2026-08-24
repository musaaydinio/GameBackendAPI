using GameBackendAPI.Data;
using GameBackendAPI.Services;
using GameBackendAPI.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. IoC  Container Yapýlandýrmasý
// Servis Interface ile somut sýnýflar eþleþtiriliyor. 
// Scoped lifetime kullanýlarak her HTTP isteði için  yeni bir nesne örneði yaratýlmasý saðlandý.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMarketService, MarketService>();

// 2. JWT Kimlik Doðrulama Katmaný  Yapýlandýrmasý
// Sistemdeki [Authorize] etiketli uç noktalara yapýlacak isteklerin Bearer þemasýyla doðrulanmasý saðlandý.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("--- JWT DOGRULAMA HATASI ---: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
