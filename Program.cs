using BodecashAPI;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Bodecash.Mapping;
using BodecashAPI.Bodecash.Persistence.Repositories;
using BodecashAPI.Bodecash.Services;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

// Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency injection
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICreditService, CreditService>();
builder.Services.AddScoped<ICreditRepository, CreditRepository>();
builder.Services.AddScoped<IInstallmentPlanService, InstallmentPlanService>();
builder.Services.AddScoped<IInstallmentPlanRepository, InstallmentPlanRepository>();
builder.Services.AddScoped<IIPPaymentService, IPPaymentService>();
builder.Services.AddScoped<IIPPaymentRepository, IPPaymentRepository>();
builder.Services.AddScoped<IIPPaymentProductService, IPPaymentProductService>();
builder.Services.AddScoped<IIPPaymentProductRepository, IPPaymentProductRepository>();
builder.Services.AddScoped<INormalPurchaseService, NormalPurchaseService>();
builder.Services.AddScoped<INormalPurchaseRepository, NormalPurchaseRepository>();
builder.Services.AddScoped<INPPurchaseService, NPPurchaseService>();
builder.Services.AddScoped<INPPurchaseRepository, NPPurchaseRepository>();
builder.Services.AddScoped<INPPurchaseProductService, NPPurchaseProductService>();
builder.Services.AddScoped<INPPurchaseProductRepository, NPPurchaseProductRepository>();
builder.Services.AddScoped<IPersonalDataService, PersonalDataService>();
builder.Services.AddScoped<IPersonalDataRepository, PersonalDataRepository>();
builder.Services.AddScoped<IShopkeeperService, ShopkeeperService>();
builder.Services.AddScoped<IShopkeeperRepository, ShopkeeperRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

// Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

/*// NO MOSTRAR SWAGGER EN PRODUCCION
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

// MOSTRAR SWAGGER EN PRODUCCION
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger at the app's root
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();