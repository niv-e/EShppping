using Catalog.Application.Commands;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productsRepostory;

    public UpdateProductHandler(IProductRepository productsRepostory)
    {
        _productsRepostory = productsRepostory;
    }


    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await _productsRepostory.UpdateProduct(new Product
        {
            Id = request.Id,
            Name = request.Name,
            Type = request.Type,
            Price = request.Price,
            Brand = request.Brand,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Summary = request.Summary,
        });

        return updateResult.Success;
    }
}

