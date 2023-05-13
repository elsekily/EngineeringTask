using System.Reflection;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using UserAPI.Core;
using UserAPI.Middleware;
using UserAPI.Persistence;
using UserAPI.Persistence.repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISecurityService, SecurityService>();


builder.Services.AddSingleton<log4net.ILog>(provider =>
{
    var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
    return LogManager.GetLogger(logRepository.Name, "UserAPI");
});

//to use only in development Environment 
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
///////////////////////


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}
app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapFallbackToFile("index.html");
});

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UserDbContext>();
    context.Database.Migrate();
}

var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));


app.Run();
