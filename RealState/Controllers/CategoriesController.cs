using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealState.Models;

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private static List<Category> categories=new List<Category>() { 
        new Category(){Id=1,Name="Name 1",ImageUrl="ImageUrl 1" },
        new Category(){Id=2,Name="Name 2",ImageUrl="ImageUrl 2" }
        };

        [HttpGet("Get")]
        public IEnumerable<Category> GET()
        {
            return categories;
        }
        [HttpPost("Insert")]
        public void Insert([FromBody]Category category)
        {
            categories.Add(category);
        }
        [HttpPut("{id}")]
        public void Put(int id,[FromBody] Category category)
        {
            categories[id]=category;
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            categories.RemoveAt(id              );
        }
    }
}
