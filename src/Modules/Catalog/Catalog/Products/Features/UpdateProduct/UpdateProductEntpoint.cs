
namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductRequest(ProductDto Product);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEntpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                UpdateProductCommand command = request.Adapt<UpdateProductCommand>();
                UpdateProductResult result = await sender.Send(command);
                UpdateProductResponse response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Prdouct");
        }
    }
}
