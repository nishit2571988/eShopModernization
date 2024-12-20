using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Catalog.Application.Commands
{
    public class DeleteProductByIdCommand:IRequest<bool>
    { 
        public string Id { get; set; }
        public DeleteProductByIdCommand(string id)
        {
            Id = id;
        }
    }
}
