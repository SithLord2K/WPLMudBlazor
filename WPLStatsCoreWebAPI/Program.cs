
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WPLBlazor.API.Data;
using WPLBlazor.API.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services.

//Add Auth0 Security

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WPLStatsDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("WPLStatsDB")));

//Build App
var app = builder.Build();

//Auth0 Security


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();

