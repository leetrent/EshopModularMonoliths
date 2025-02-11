namespace Catalog.Products.Features.CreateProduct;

public record CreateProductResult(Guid Id);

public record CreateProductCommand(ProductDto Product)
    : ICommand<CreateProductResult>;

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Product name is required");
        RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Product category is required");
        RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("Product image file is required");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Product price must be greater than 0");
    }
}

public class CreateProductHandler
    (
        CatalogDbContext dbContext, 
        ILogger<CreateProductHandler> logger
    )
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        ///////////////////////////////////////////////////////////////////////////////////
        // LOGGING
        ///////////////////////////////////////////////////////////////////////////////////
        logger.LogInformation("CreateProductHandler.Handle called with {$Commane}", command);

        Product product = CreateNewProduct(command.Product);
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto dto)
    {
        return Product.Create
        (
            Guid.NewGuid(),
            dto.Name,
            dto.Category,
            dto.Description,
            dto.ImageFile,
            dto.Price
        );
    }
}