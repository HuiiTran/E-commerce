using ProductApi.Entities;
using ServicesCommon.MassTransit;
using ServicesCommon.MongoDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


//Add repository
builder.Services.AddMongo()
    .AddMongoRepository<Product>("Products")
    .AddMongoRepository<ProductType>("ProductType")
    .AddMongoRepository<ProductBrand>("ProductBrand")
    .AddMongoRepository<ProductBrand>("ProductOperatingSystem")
    .AddMongoRepository<ProductBrand>("ProductConnectivity")
    .AddMongoRepository<ProductBrand>("ProductBatteryCapacity")
    .AddMongoRepository<ProductBrand>("ProductNetworkType")
    .AddMongoRepository<ProductBrand>("ProductRam")
    .AddMongoRepository<ProductBrand>("ProductStorage")
    .AddMongoRepository<ProductBrand>("ProductResolution")
    .AddMongoRepository<ProductBrand>("ProductRefeshRate")
    .AddMongoRepository<ProductBrand>("ProductSpecialFeature")

    .AddMassTransitWithRabbitMq();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
