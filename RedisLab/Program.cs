using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RedisLab.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRazorPages();

var sqlCnn = builder.Configuration.GetConnectionString("sqlCnn");
builder.Services.AddDbContext<LabContext>(o => o.UseSqlServer(sqlCnn));

var redisCnn = builder.Configuration.GetConnectionString("redisCnn");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisCnn;
    options.InstanceName = "RedisLab";
});

//builder.Services.AddScoped<IUsuarioRepository, UsuarioSqlRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRedisRepository>(x =>
        new UsuarioRedisRepository(x.GetService<IDistributedCache>(),
            new UsuarioSqlRepository(x.GetService<LabContext>())
        )
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();