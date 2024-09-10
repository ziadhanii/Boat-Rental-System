using BoatRentalSystem.API.Controllers;
using BoatRentalSystem.Core.Interfaces;
using BoatSystem.Application.Commands.AdditionalServiceCommand;
using BoatSystem.Application.Commands.BoatCommands;
using BoatSystem.Application.Commands.CityCommands.Add;
using BoatSystem.Application.Commands.UserCommands;
using BoatSystem.Application.Services;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using BoatSystem.Core.Repositories;
using BoatSystem.Infrastructure;
using BoatSystem.Infrastructure.Data;
using BoatSystem.Infrastructure.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

// Configure Serilog
ConfigureSerilog(builder.Host);

// Build and configure the application
var app = builder.Build();
ConfigureApp(app);

app.Run();

// Methods to organize configuration
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(SwaggerDocsConstant.Admin, new OpenApiInfo { Title = "Admin API", Version = "v1" });
        c.SwaggerDoc(SwaggerDocsConstant.Owner, new OpenApiInfo { Title = "Owner API", Version = "v1" });
        c.SwaggerDoc(SwaggerDocsConstant.Customer, new OpenApiInfo { Title = "Customer API", Version = "v1" });
    });

    // Register repositories and services
    services.AddScoped<ICityRepository, CityRepository>();
    services.AddScoped<ICountryRepository, CountryRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IBoatRepository, BoatRepository>();
    services.AddScoped<ITripRepository, TripRepository>();
    services.AddScoped<IReservationRepository, ReservationRepository>();
    services.AddScoped<ICustomerRepository, CustomerRepository>();
    services.AddScoped<IAdminService, AdminService>();
    services.AddScoped<IOwnerRepository, OwnerRepository>();
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IBoatService, BoatService>();
    services.AddScoped<ITripService, TripService>();
    services.AddScoped<IAdditionalService, AdditionalService>();
    services.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();
    services.AddTransient<IAdditionalService, AdditionalService>();
    services.AddTransient<IAdditionalServiceRepository, AdditionalServiceRepository>();
    //services.AddScoped<IBookingService, BookingService>();
    //services.AddScoped<IBookingRepository, BookingRepository>();
    //services.AddScoped<IBookingService, BookingService>();
    services.AddScoped<ICustomerService, CustomerService>();

    services.AddAutoMapper(typeof(MappingProfile));

    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    services.AddHangfire(config =>
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        config.UseSqlServerStorage(connectionString);
    });

    services.Configure<JWT>(configuration.GetSection("JWT"));

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
        };
    });
    //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddMediatR(c =>
    {
        c.RegisterServicesFromAssemblies(
           Assembly.GetAssembly(typeof(AddCityCommand)),
           Assembly.GetAssembly(typeof(RejectUserRegistrationCommand)),
           Assembly.GetAssembly(typeof(ApproveUserRegistrationCommand)),
           Assembly.GetAssembly(typeof(RejectUserRegistrationCommandHandler)),
           Assembly.GetAssembly(typeof(ApproveUserRegistrationCommandHandler)),
           Assembly.GetAssembly(typeof(RejectBoatRegistrationCommand)),
           Assembly.GetAssembly(typeof(RejectBoatRegistrationCommandHandler)),
           Assembly.GetAssembly(typeof(CreateAdditionalServiceCommand)),
           Assembly.GetAssembly(typeof(UpdateAdditionalServiceCommand)),
           Assembly.GetAssembly(typeof(DeleteAdditionalServiceCommand))
        );
    });

}

void ConfigureSerilog(IHostBuilder hostBuilder)
{
    var logDirectory = $"logs\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
    if (!Directory.Exists(logDirectory))
    {
        Directory.CreateDirectory(logDirectory);
    }

    hostBuilder.UseSerilog((ctx, lc) => lc
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Information)
        .WriteTo.File(
           path: Path.Combine(logDirectory, "logs.json"),
           rollingInterval: RollingInterval.Day,
           outputTemplate: "{Timestamp} {Message} {NewLine:1} {Exception:!}"
        ));
}

void ConfigureApp(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{SwaggerDocsConstant.Admin}/swagger.json", "Admin API V1");
            c.SwaggerEndpoint($"/swagger/{SwaggerDocsConstant.Owner}/swagger.json", "Owner API V1");
            c.SwaggerEndpoint($"/swagger/{SwaggerDocsConstant.Customer}/swagger.json", "Customer API V1");
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseHangfireDashboard("/dashboard");
    // Uncomment this line to start the Hangfire server
    // app.UseHangfireServer();
}


//using BoatRentalSystem.API.Controllers;
//using BoatRentalSystem.Application;
//using BoatRentalSystem.Core.Interfaces;
//using BoatRentalSystem.Infrastructure;
//using Microsoft.EntityFrameworkCore;
//using Serilog;
//using Hangfire;
//using BoatSystem.Infrastructure;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;
//using BoatSystem.Core.Interfaces;
//using BoatSystem.Application;
//using BoatSystem.Core.Models;
//using BoatSystem.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.OpenApi.Models;
//using System.Reflection;
//using BoatRentalSystem.Core.Entities;
//using BoatSystem.Infrastructure.Data;
//using BoatSystem.Application.Commands.CityCommands.Add;
//using BoatSystem.Infrastructure.Repositories;
//using BoatSystem.Application.Commands.UserCommands;
//using BoatSystem.Application.Commands.BoatCommands;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("user", new OpenApiInfo { Title = "User API", Version = "v1" });
//    options.SwaggerDoc("admin", new OpenApiInfo { Title = "Admin API", Version = "v1" });
//});

//builder.Services.AddScoped<ICityRepository, CityRepository>();
//builder.Services.AddScoped<CityService>();

//builder.Services.AddScoped<ICountryRepository, CountryRepository>();
//builder.Services.AddScoped<CountryService>();

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IBoatRepository, BoatRepository>();
//builder.Services.AddScoped<ITripRepository, TripRepository>();
//builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

//builder.Services.AddScoped<IAdminService, AdminService>();
////builder.Services.AddScoped<IOwnerService, OwnerService>(); // تحقق من وجود هذا التعريف
////builder.Services.AddScoped<ICustomerService, CustomerService>();
//// builder.Services.AddScoped<IBookingService, BookingService>(); // تحقق من وجود التعريف الخاص بالخدمة
//builder.Services.AddScoped<ICityRepository, CityRepository>();
//builder.Services.AddScoped<CityService>();
//builder.Services.AddScoped<ICountryRepository, CountryRepository>();
//builder.Services.AddScoped<CountryService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IBoatRepository, BoatRepository>();
//builder.Services.AddScoped<ITripRepository, TripRepository>();
//builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//builder.Services.AddScoped<IAdminService, AdminService>();
////builder.Services.AddScoped<IOwnerService, OwnerService>();
////builder.Services.AddScoped<ICustomerService, CustomerService>();



//builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();



//builder.Services.AddAutoMapper(typeof(MappingProfile));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddScoped<IAuthService, AuthService>();



//var logDictionary = $"logs\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}";
//if (!Directory.Exists(logDictionary))
//{
//    Directory.CreateDirectory(logDictionary);
//}

//builder.Host.UseSerilog((ctx, lc) => lc
//    .MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.Seq("http://localhost:5341", Serilog.Events.LogEventLevel.Information)
//    .WriteTo.File(
//       path: Path.Combine(logDictionary, "logs.json"),
//       rollingInterval: RollingInterval.Day,
//       outputTemplate: "{Timestamp} {Message} {NewLine:1} {Exception:!}"
//    ));


//builder.Services.AddHangfire(config =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    config.UseSqlServerStorage(connectionString);

//});




//builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//   .AddJwtBearer(o =>
//   {
//       o.RequireHttpsMetadata = false;
//       o.SaveToken = false;
//       o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//       {
//           ValidateIssuerSigningKey = true,
//           ValidateIssuer = true,
//           ValidateAudience = true,
//           ValidateLifetime = true,
//           ValidIssuer = builder.Configuration["JWT:Issuer"],
//           ValidAudience = builder.Configuration["JWT:Audience"],
//           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]
//           ))
//       };
//   });

//builder.Services.AddMediatR(c =>
//{
//    c.RegisterServicesFromAssemblies(
//        Assembly.GetAssembly(typeof(AddCityCommand)),
//        Assembly.GetAssembly(typeof(City)),
//        Assembly.GetAssembly(typeof(RejectUserRegistrationCommand)),
//        Assembly.GetAssembly(typeof(ApproveUserRegistrationCommand)),
//        Assembly.GetAssembly(typeof(RejectUserRegistrationCommandHandler)),
//        Assembly.GetAssembly(typeof(ApproveUserRegistrationCommandHandler)),
//        Assembly.GetAssembly(typeof(RejectBoatRegistrationCommand)),
//        Assembly.GetAssembly(typeof(RejectBoatRegistrationCommandHandler))
//    );
//});



//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint($"/swagger/{SwaggerDocsConstant.Admin}/swagger.json", "Admin API");
//        c.SwaggerEndpoint($"/swagger/{SwaggerDocsConstant.User}/swagger.json", "User API");
//    }
//        );
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.UseHangfireDashboard("/dashboard");
////app.UseHangfireServer();

//app.Run();
