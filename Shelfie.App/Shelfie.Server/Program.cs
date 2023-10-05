using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shelfie.Server;
using Shelfie.Server.Data;

var seed = args.Contains("/seed");
if(seed)
{
    args = args.Except(new[] {"/seed"}).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (seed)
{    
    SeedData.EnsureSeedData(connectionString);
}

var assembly = typeof(Program).Assembly.GetName().Name;

builder.Services.AddDbContext<AspNetIdentityDbContext>(options => 
    options.UseSqlServer(connectionString,
        sql => sql.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AspNetIdentityDbContext>();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseSqlServer(connectionString,
                           sql => sql.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseSqlServer(connectionString,
                           sql => sql.MigrationsAssembly(assembly));
    })
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseRouting();
app.UseIdentityServer();

app.Run();
