var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
