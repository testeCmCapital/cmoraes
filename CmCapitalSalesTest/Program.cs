using CmCapitalSalesAvaliacao.Infra.Configuration;
using CmCapitalSalesAvaliacao.Infra.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

builder.Services.AddDbContext<CmCapitalSalesContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
        optional: false,
        reloadOnChange: true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/topSellingProducts", () =>
{
    return "Produtos mais vendidos";
});

app.MapGet("/leastSoldProducts", () =>
{
    return "Produtos menos vendidos";
});


app.MapGet("/clientPurchasedProducts", () =>
{
    return "Produtos comprados por clientes";
});

app.MapPost("/BuyProduct", () =>
{
    return "Produto comprado";
});

app.Run();
