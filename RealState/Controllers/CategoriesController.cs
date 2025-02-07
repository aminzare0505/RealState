using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Data;
using RealState.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RealState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDBContext _dbContext = new ApiDBContext();
        // GET: api/<CategoriesController>
       [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }
        [HttpGet("[action]")]//with this attribute, you can call the method with names of method 
        public IActionResult GetCategoriesWithSort()
        {
            return Ok(_dbContext.Categories.OrderByDescending(o=>o.Name));
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.Categories.FirstOrDefault(f=>f.Id==id));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            _dbContext.Categories.Add(value);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category categoryObj)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("No Record Found");
            }
            else
            {
                category.Name = categoryObj.Name;
                category.ImageUrl = categoryObj.ImageUrl;
                _dbContext.SaveChanges();
                return Ok("Record Updated Succesfully");
            }
        }
        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category= _dbContext.Categories.Find(id);
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            return Ok("Record Deleted");

        }
    }
}
