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

        [HttpPost] //create item
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

        [HttpGet] //get all item
        public IActionResult GetProducts()
        {
            List<Product> products = new List<Product>();
            try{
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM products";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.Id = (int)reader["id"];
                            product.Name = (string)reader["name"];
                            product.Brand = (string)reader["brand"];
                            product.Category = (string)reader["category"];
                            product.Price = (decimal)reader["price"];
                            product.Description = (string)reader["description"];
                            product.CreatedAt = (DateTime)reader["created_at"];

                            products.Add(product); //add the above product to the list
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(products);
        }

        [HttpGet("{id}")] //get item by id
        public IActionResult GetProductById(int id)
        {
            Product product = new Product();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM products WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            product.Id = (int)reader["id"];
                            product.Name = (string)reader["name"];
                            product.Brand = (string)reader["brand"];
                            product.Category = (string)reader["category"];
                            product.Price = (decimal)reader["price"];
                            product.Description = (string)reader["description"];
                            product.CreatedAt = (DateTime)reader["created_at"];
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", ex.Message);
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [HttpPut("{id}")] //update item by id
        public IActionResult UpdateProduct(int id, ProductDto productDto)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE products SET Name = @name, Brand = @brand, Category = @category, Price = @price, Description = @description WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
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
            }
            return Ok();
        }

        [HttpDelete("{id}")] //delete item by id
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM products WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Product", ex.Message);
                return BadRequest(ModelState);
            }
            return Ok();
        }
    }
}
