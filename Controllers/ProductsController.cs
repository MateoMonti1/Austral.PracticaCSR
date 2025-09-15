using Microsoft.AspNetCore.Mvc;
using PracticaCSR.Models.DTOs;
using PracticaCSR.Services.Implementations;

namespace PracticaCSR.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService = new ProductService();

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _productService.GetAllProducts();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var productDto = _productService.GetProductById(id);

        return Ok(productDto);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] ProductForCreateDto dto)
    {
        var productDto = _productService.CreateProduct(dto);

        return Created("El producto fue creado exisosamente", productDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] ProductForUpdateDto dto)
    {
        _productService.UpdateProduct(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);

        return NoContent();
    }
}
