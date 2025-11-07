using SentinelCore.API.Extensions;
using Microsoft.EntityFrameworkCore;
using SentinelCore.Infrastructure.Seed;
using SentinelCore.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    DbInitializer.Seed(db);
}
app.UseCustomMiddlewares();
app.UseHttpsRedirection();

app.Run();