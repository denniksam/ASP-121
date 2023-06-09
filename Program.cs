using ASP121.Data;
using ASP121.Middleware;
using ASP121.Services.Cosmos;
using ASP121.Services.Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHashService, Md5HashService>();
builder.Services.AddSingleton<ICosmosDbService, TodoCosmosDbService>();

// Add Data Context
builder.Services.AddDbContext<DataContext>(options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("PlanetDb"),
        new MySqlServerVersion(new Version(8, 0, 23)),
        serverOptions =>
            serverOptions
                .MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: "ASP121")
                .SchemaBehavior(
                    MySqlSchemaBehavior.Translate,
                    (schema, table) => $"{schema}_{table}")
));

// ������������ ����:
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();  // ��������� ����

app.UseSessionAuth();   // ������� Middleware �� ��������, SessionAuth ��� ���� UseSession

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
