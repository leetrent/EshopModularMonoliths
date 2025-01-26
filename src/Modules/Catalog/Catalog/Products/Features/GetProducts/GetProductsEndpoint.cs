
namespace Catalog.Products.Features.GetProducts
{
    /////////////////////////////////////
    // NOT NEEDED FOR GET ALL PRODUCTS
    // public record GetProducsRequest();
    /////////////////////////////////////
    
    public record GetProductsResponse(IEnumerable<ProductDto> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                GetProductsResult result = await sender.Send(new GetProductsQuery());
                GetProductsResponse response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");

        }
    }
}
