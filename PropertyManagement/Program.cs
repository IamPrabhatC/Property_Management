using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.Api.Mappings;
using PropertyManagement.Api.Middleware;
using PropertyManagement.Application.Interfaces;
using PropertyManagement.Application.Services;
using PropertyManagement.Domain.Interfaces;
using PropertyManagement.Domain.Services;
using PropertyManagement.Infrastructure.Persistence;
using PropertyManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ServiceBusClient>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("ServiceBusConnection");
    return new ServiceBusClient(connectionString);
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 

// Register repositories
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register domain services
builder.Services.AddScoped<IPropertyDomainService, PropertyDomainService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

// Register application services
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddAutoMapper(typeof(PropertyMappingProfile));
builder.Services.AddAutoMapper(typeof(AddressMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global exception handling middleware we can customize it more
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
