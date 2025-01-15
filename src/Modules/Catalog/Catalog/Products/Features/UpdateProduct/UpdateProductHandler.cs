
namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductResult(bool IsSuccess);
    public record UpdateProductCommand(ProductDto Product)
        : ICommand<UpdateProductResult>;


    public class UpdateProductHandler(CatalogDbContext dbContext)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products.FindAsync([command.Product.Id], cancellationToken: cancellationToken);
            if (product is null)
            {
                throw new Exception($"Product not found with ID of '{command.Product.Id}'. Cannot update product.");
            }

            UpdateProductWithNewValues(product, command.Product);
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }

        private void UpdateProductWithNewValues(Product product, ProductDto dto)
        {
            product.Update
            (
                dto.Name,
                dto.Category,
                dto.Description,
                dto.ImageFile,
                dto.Price
            );
        }
    }
}
