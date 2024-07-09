using HahnCargoAutomation.Server.Data;
using HahnCargoAutomation.Server.Entities;
using HahnSimBack.Dtos;
using HahnSimBack.Interfaces;
using HahnSimBack.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("defaultConnection") ?? throw new InvalidOperationException("cannot find connection string!");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    object value = options.UseSqlServer(connectionString);
});

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<CargoSimClientOptionsDto>(builder.Configuration.GetSection("CargoSimClientOptions"));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();

builder.Services.AddSingleton<ICachingTokenService, CachingTokenService>();
builder.Services.AddSingleton<ICargoSimService, CargoSimService>();

builder.Services.AddHttpClient<ICachingTokenService, CachingTokenService>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<CargoSimClientOptionsDto>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

builder.Services.AddHttpClient<ICargoSimService, CargoSimService>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<CargoSimClientOptionsDto>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);

}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});

var app = builder.Build();

app.UseCors(policy => policy
    .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseExceptionHandler(options => { });
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapIdentityApi<User>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

