using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PfeProject.Application.Interfaces;
using PfeProject.Application.Service;
using PfeProject.Application.Services;
using PfeProject.Domain.Interfaces;
using PfeProject.Infrastructure.Persistence;
using PfeProject.Infrastructure.Repositories;
using PfeProject.API.Middlewares;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// ===== 1. Connexion PostgreSQL =====
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// ===== 2. Dependency Injection =====
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<IPicklistService, PicklistService>();
builder.Services.AddScoped<IPicklistRepository, PicklistRepository>();
builder.Services.AddScoped<IReturnLineService, ReturnLineService>();
builder.Services.AddScoped<IReturnLineRepository, ReturnLineRepository>();
builder.Services.AddScoped<IDetailPicklistService, DetailPicklistService>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ISapService, SapService>();
builder.Services.AddScoped<ISapRepository,SapRepository>();
builder.Services.AddScoped<IDetailInventoryRepository, DetailInventoryRepository>();
builder.Services.AddScoped<IDetailInventoryService, DetailInventoryService>();
builder.Services.AddScoped<IDetailPicklistService, DetailPicklistService>();

builder.Services.AddScoped<IDetailPicklistRepository, DetailPicklistRepository>();
builder.Services.AddScoped<IMovementTraceRepository, MovementTraceRepository>();
builder.Services.AddScoped<IMovementTraceService, MovementTraceService>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ILineRepository, LineRepository>();
builder.Services.AddScoped<ILineService, LineService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IPicklistUsRepository, PicklistUsRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IPicklistUsService, PicklistUsService>();



// ===== 3. Authentification JWT =====
var jwtSettings = configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

// ===== 4. Swagger + JWT support =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "PFE API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Saisir 'Bearer {token}'"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ===== 5. Configuration CORS (ajout pour Frontend Angular) =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

var app = builder.Build();

// ===== 6. Seeding Data (création admin, rôles...) =====
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}

// ===== 7. Middleware pipeline =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 👇 IMPORTANT : CORS doit être avant Authentication
app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

// Add company data isolation middleware
app.UseMiddleware<CompanyDataIsolationMiddleware>();

app.MapControllers();

app.Run();
