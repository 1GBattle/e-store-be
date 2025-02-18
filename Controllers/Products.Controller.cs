using e_store_be.Data;
using e_store_be.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_store_be.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(StoreContext context) : ControllerBase
{
    // GETS All the products from db
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return await context.Products.ToListAsync();
    }

    //Get product by ID
    [HttpGet("{id:int}")]
    public async Task<IResult> GetProduct(int id)
    {
        var product = await context.Products.FindAsync(id);

        return product == null ? Results.NotFound() : Results.Ok(product);
    }
}
