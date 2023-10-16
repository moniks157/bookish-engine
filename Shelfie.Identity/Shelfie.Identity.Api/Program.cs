using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shelfie.Identity.Api.Validators;
using Shelfie.Identity.BusinessLogic.Services;
using Shelfie.Identity.BusinessLogic.Services.Interfaces;
using Shelfie.Identity.BusinessLogic.UseCases.LoginUser;
using Shelfie.Identity.DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(LoginUserCommand).Assembly;
ConfigurationManager configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

//Register DbContext
builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

//Register Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>()
    .AddDefaultTokenProviders();

//Register Validators
builder.Services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();

//Register MeddiatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

//Register Services
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddControllers();

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
