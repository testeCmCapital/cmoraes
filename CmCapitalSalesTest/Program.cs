var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
