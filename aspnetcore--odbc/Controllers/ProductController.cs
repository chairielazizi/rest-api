using aspnetcore__odbc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace aspnetcore__odbc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string? connectionString;

        public ProductController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO products (Name, Brand, Category, Price, Description) VALUES (@name, @brand, @category, @price, @description)";
                    command.Parameters.AddWithValue("@name", productDto.Name);
                    command.Parameters.AddWithValue("@brand", productDto.Brand);
                    command.Parameters.AddWithValue("@category", productDto.Category);
                    command.Parameters.AddWithValue("@price", productDto.Price);
                    command.Parameters.AddWithValue("@description", productDto.Description);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", ex.Message);
                return BadRequest(ModelState);
                // return StatusCode(500, ex.Message);
            }
            return Ok();
        }
    }
}
