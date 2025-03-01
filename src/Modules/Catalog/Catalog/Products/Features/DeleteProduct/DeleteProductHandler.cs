
using Catalog.Products.Features.UpdateProduct;

namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductResult(bool IsSuccess);

public record DeleteProductCommand(Guid ProductId)
    : ICommand<DeleteProductResult>;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required");
    }
}

public class DeleteProductHandler(CatalogDbContext dbContext)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await dbContext.Products.FindAsync([command.ProductId], cancellationToken: cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}