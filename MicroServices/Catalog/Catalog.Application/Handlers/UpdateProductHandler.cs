using Catalog.Application.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        { 
            //object Mapping example we can also do using AUTO MAPPER
            var productEntity = await _productRepository.UpdateProduct(new Product { 
                Id = request.Id,  
                Name = request.Name,
                Summary = request.Summary,  
                Description = request.Description,  
                ImageFile= request.ImageFile,   
                Price = request.Price,  
                Brands = request.Brands,
                Types  = request.Types
            });
            return productEntity;
        }
    }
}
