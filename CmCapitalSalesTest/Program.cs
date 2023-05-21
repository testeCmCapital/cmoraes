using CmCapitalSalesAvaliacao.Infra.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();



builder.Services.RegisterServices();

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
