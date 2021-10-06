using MediatR;
using Product.Microservices.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Microservices.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationContext _context;
            public CreateProductCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product.Microservices.Models.Product();
                product.Name = command.Name;
                product.Price = command.Price;
                product.Description = command.Description;
                _context.Products.Add(product);
                await _context.SaveChanges();
                return product.Id;
            }
        }
    }
}
