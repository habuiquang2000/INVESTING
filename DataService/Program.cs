using DataService.Caches;
using DataService.Services;
using SqlExecute.Connect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//    var builder = new ConfigurationBuilder()
//        .SetBasePath(env.ContentRootPath)
//        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
//        .AddEnvironmentVariables();
//    _config = builder.Build();
//var connectionString = _config.GetConnectionString("DbContextSettings:ConnectionString");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
SqlConnect.ConnectionString = connectionString;


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

ProductCache.ProductList = ProductService.ReadMany();