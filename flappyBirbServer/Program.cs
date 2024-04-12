using flappyBirb_server.Data;
using flappyBirb_server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FlappyBirbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlappyBirbContext"));
    options.UseLazyLoadingProxies();
});
builder.Services.AddIdentity<BirbUser, IdentityRole>().AddEntityFrameworkStores<FlappyBirbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
