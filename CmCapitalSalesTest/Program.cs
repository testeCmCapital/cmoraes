using CmCapitalSalesAvaliacao.Domain.DTOs;
using CmCapitalSalesAvaliacao.Domain.Services;
using CmCapitalSalesAvaliacao.Infra.Configuration;
using CmCapitalSalesAvaliacao.Infra.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

builder.Services.AddDbContext<CmCapitalSalesDbContext>(options =>
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

app.MapGet("/topSellingProducts", (CmCapitalSalesService cmCapitalService) =>
{
    var returnData = cmCapitalService.ListarProdutosMaisVendidos();

    return returnData.IsSuccess ? Results.Ok(returnData) : Results.BadRequest(returnData);
});

app.MapGet("/leastSoldProducts", (CmCapitalSalesService cmCapitalService) =>
{
    var returnData = cmCapitalService.ListarProdutosMenosVendidos();

    return returnData.IsSuccess ? Results.Ok(returnData) : Results.BadRequest(returnData);
});


app.MapGet("/clientPurchasedProducts", (CmCapitalSalesService cmCapitalService, int CdCliente) =>
{
    var returnData = cmCapitalService.ListarProdutosCompradosPorCliente(CdCliente);

    return returnData.IsSuccess ? Results.Ok(returnData) : Results.BadRequest(returnData);
});

app.MapPost("/buyProduct", (CmCapitalSalesService cmCapitalService, PedidoDTO PedidoDTO) =>
{
    var returnData = cmCapitalService.EfetivarPedido(PedidoDTO);

    return returnData.IsSuccess ? Results.Ok(returnData) : Results.BadRequest(returnData);
});

app.MapPut("/cancelPurshase", (CmCapitalSalesService cmCapitalService, int CdPedido) =>
{
    var returnData = cmCapitalService.CancelarPedido(CdPedido);
    return returnData.IsSuccess ? Results.Ok(returnData) : Results.BadRequest(returnData);
});





app.Run();
