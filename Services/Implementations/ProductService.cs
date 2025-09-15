using PracticaCSR.Entities;
using PracticaCSR.Models.DTOs;
using PracticaCSR.Repositories.Implementations;
using PracticaCSR.Services.Interfaces;

namespace PracticaCSR.Services.Implementations;

public class ProductService : IProductService
{
    private readonly ProductRepository _productRepository = new ProductRepository();

    public List<ProductForReadDto> GetAllProducts()
    {
        return _productRepository.GetAllProducts().
            Select(p => new ProductForReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
            }).ToList();
    }

    public ProductForReadDto? GetProductById(int id)
    {
        Product? product = _productRepository.GetProductById(id);

        if (product is null)
        {
            throw new InvalidOperationException("El producto no existe.");
        }

        ProductForReadDto productDto = new ProductForReadDto
        {
            Name = product.Name,
            Price = product.Price,
        };

        return productDto;
    }

    public ProductForReadDto CreateProduct(ProductForCreateDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price
        };

        var createdProduct = _productRepository.AddProduct(product);

        return new ProductForReadDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price
        };
    }

    public void DeleteProduct(int id)
    {
        Product? product = _productRepository.GetProductById(id);

        if (product is null)
        {
            throw new InvalidOperationException("El producto que se quiere borrar no existe.");
        }

        _productRepository.DeleteProduct(product);
    }

    public void UpdateProduct(int id, ProductForUpdateDto dto)
    {
        var product = _productRepository.GetProductById(id);

        if (product is null)
        {
            throw new InvalidOperationException("El producto que se quiere actualizar no existe.");
        }

        product.Name = dto.Name;
        product.Price = dto.Price;

        _productRepository.UpdateProduct(product);
    }
}
