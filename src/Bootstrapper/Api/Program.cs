WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddCarterWithAssemblies(typeof(CatalogModule).Assembly);
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

WebApplication app = builder.Build();
app.MapCarter();
app.UseCatalogModule();
app.UseBasketModule();
app.UseOrderingModule();
app.Run();
