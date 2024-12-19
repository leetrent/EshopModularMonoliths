var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();
app.UseCatalogModule();
app.UseBasketModule();
app.UseOrderingModule();

//app.MapGet("/", () => "Hello World!");

app.Run();
