
namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdResult(ProductDto Product);
public record GetProductByIdQuery(Guid Id)
    : IQuery<GetProductByIdResult>;
public class GetProductByIdHandler(CatalogDbContext dbContext)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product not found of ID: '{query.Id}'");
        }

        var productDto = product.Adapt<ProductDto>();
        return new GetProductByIdResult(productDto);
    }
}
