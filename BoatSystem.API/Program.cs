using BoatSystem.Application.Commands.AdditionalServiceCommand;
using BoatSystem.Application.Commands.BoatCommands;
using BoatSystem.Application.Commands.UserCommands;
using BoatSystem.Application.Commands.Wallet;
using BoatSystem.Application.Services;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using BoatSystem.Core.Repositories;
using BoatSystem.Infrastructure;
using BoatSystem.Infrastructure.Data;
using BoatSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

ConfigureSerilog(builder.Host);

var app = builder.Build();
ConfigureApp(app);

app.Run();

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
    services.AddScoped<IBookingRepository, BookingRepository>();
    services.AddScoped<ICustomerService, CustomerService>();
    services.AddScoped<IBookingAdditionRepository, BookingAdditionRepository>();
    services.AddScoped<ICostCalculatorService, CostCalculatorService>();
    services.AddScoped<IWalletRepository, WalletRepository>(); 
    builder.Services.AddScoped<IBookingRepository, BookingRepository>();
    builder.Services.AddScoped<ICancellationRepository, CancellationRepository>();
    services.AddScoped<IBookingRepository, BookingRepository>();
    services.AddScoped<ICancellationRepository, CancellationRepository>();
    services.AddScoped<IBoatBookingRepository, BoatBookingRepository>();
    services.AddScoped<IBookingService, BookingService>();

    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

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

    services.AddMediatR(c =>
    {
        c.RegisterServicesFromAssemblies(
           Assembly.GetAssembly(typeof(RejectUserRegistrationCommand)),
           Assembly.GetAssembly(typeof(ApproveUserRegistrationCommand)),
           Assembly.GetAssembly(typeof(RejectUserRegistrationCommandHandler)),
           Assembly.GetAssembly(typeof(ApproveUserRegistrationCommandHandler)),
           Assembly.GetAssembly(typeof(RejectBoatRegistrationCommand)),
           Assembly.GetAssembly(typeof(RejectBoatRegistrationCommandHandler)),
           Assembly.GetAssembly(typeof(CreateAdditionalServiceCommand)),
           Assembly.GetAssembly(typeof(UpdateAdditionalServiceCommand)),
           Assembly.GetAssembly(typeof(DeleteAdditionalServiceCommand))
           //Assembly.GetAssembly(typeof(BookTripCommand))
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

}