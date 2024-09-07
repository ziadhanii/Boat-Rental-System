using BoatRentalSystem.API.Controllers;
using BoatRentalSystem.Application;
using BoatRentalSystem.Core.Interfaces;
using BoatRentalSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Hangfire;
using BoatSystem.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BoatSystem.Core.Interfaces;
using BoatSystem.Application;
using BoatSystem.Core.Models;
using BoatSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using BoatRentalSystem.Core.Entities;
using BoatSystem.Infrastructure.Data;
using BoatSystem.Application.Commands.CityCommands.Add;
using BoatSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("user", new OpenApiInfo { Title = "User API", Version = "v1" });
    options.SwaggerDoc("admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });
});

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<CityService>();

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<CountryService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBoatRepository, BoatRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IAdminService, AdminService>();
//builder.Services.AddScoped<IOwnerService, OwnerService>(); // تحقق من وجود هذا التعريف
//builder.Services.AddScoped<ICustomerService, CustomerService>();
// builder.Services.AddScoped<IBookingService, BookingService>(); // تحقق من وجود التعريف الخاص بالخدمة
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<CityService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<CountryService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBoatRepository, BoatRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
//builder.Services.AddScoped<IOwnerService, OwnerService>();
//builder.Services.AddScoped<ICustomerService, CustomerService>();



builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();



builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();



var logDictionary = $"logs\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
if (!Directory.Exists(logDictionary))
{
    Directory.CreateDirectory(logDictionary);
}

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Information)
    .WriteTo.File(
       path: Path.Combine(logDictionary, "logs.json"),
       rollingInterval: RollingInterval.Day,
       outputTemplate: "{Timestamp} {Message} {NewLine:1} {Exception:!}"
    ));


builder.Services.AddHangfire(config =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(connectionString);

});




builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(o =>
   {
       o.RequireHttpsMetadata = false;
       o.SaveToken = false;
       o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidIssuer = builder.Configuration["JWT:Issuer"],
           ValidAudience = builder.Configuration["JWT:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]
           ))
       };
   });


builder.Services.AddMediatR(c =>
    {
        c.RegisterServicesFromAssemblies(
            Assembly.GetAssembly(typeof(AddCityCommand)),
            Assembly.GetAssembly(typeof(City))
            );
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/{SwaggerDocsConstant.Admin}/swagger.json", "Admin API");
        c.SwaggerEndpoint($"/swagger/{SwaggerDocsConstant.User}/swagger.json", "User API");
    }
        );
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/dashboard");
//app.UseHangfireServer();

app.Run();
