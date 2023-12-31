using CasgemMicroservice.Services.Cargo.BussinessLayer.Abstarct;
using CasgemMicroservice.Services.Cargo.BussinessLayer.Concrete;
using CasgemMicroservice.Services.Cargo.DataAccessLayer.Abstract;
using CasgemMicroservice.Services.Cargo.DataAccessLayer.Context;
using CasgemMicroservice.Services.Cargo.DataAccessLayer.EntityFramwork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "resource_cargo";
    opt.RequireHttpsMetadata = false;
});
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CargoContext>();
builder.Services.AddScoped<ICargoDetailServices, CargoDetailManger>();
builder.Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>();

builder.Services.AddScoped<ICargoStatusServices, CargoStatusManager>();
builder.Services.AddScoped<ICargoStateDal, EfCargoStateDal>();

builder.Services.AddControllers(opt =>
{
    //bu komut kullan�c�y� giri� yapmaya zorluyan k�s�m
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
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
