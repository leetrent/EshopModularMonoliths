
namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductResult(bool IsSuccess);

public record DeleteProductCommand(Guid ProductId)
    : ICommand<DeleteProductResult>;
public class DeleteProductHandler(CatalogDbContext dbContext)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Products.FindAsync([command.ProductId], cancellationToken: cancellationToken);
        if (product is null)
        {
            throw new Exception($"Product not found with ID of '{command.ProductId}'. Cannot delete product.");
        }
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}