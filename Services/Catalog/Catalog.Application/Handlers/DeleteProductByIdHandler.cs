using Catalog.Application.Commands;
using MediatR;

namespace Catalog.Application.Handlers;

public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, bool>
{
    private readonly IProductRepository _productsRepostory;

    public DeleteProductByIdHandler(IProductRepository productsRepostory)
    {
        _productsRepostory = productsRepostory;
    }


    public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var updateResult = await _productsRepostory.DeleteProduct(request.Id);

        return updateResult.Success;
    }
}

