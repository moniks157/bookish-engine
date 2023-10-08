using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Shelfie.Domain.UseCases.Books.CreateBook;
using Shelfie.Repository;
using Shelfie.Repository.Repositories;
using Shelfie.Repository.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ShelfieDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateBookCommand).Assembly));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer").AddIdentityServerAuthentication("Bearer", options =>
{
    options.Authority = "https://localhost:5443";
    options.RequireHttpsMetadata = false;
    options.ApiName = "shelfie-api";
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
