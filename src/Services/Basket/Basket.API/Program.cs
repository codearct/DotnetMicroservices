var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//BuildIn
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Application Services
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//External Services
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = builder.Configuration
    .GetValue<string>("CacheSettings:ConnectionString");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
