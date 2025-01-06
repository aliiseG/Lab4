using Lab2.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookbooksController : ControllerBase
    {
        
        private RecipeDbContext _db;

        public CookbooksController()
        {
            _db = new RecipeDbContext();
        }

        [HttpGet]
        public Cookbook[] GetCookbooks()
        {
            var data = _db.Cookbooks.ToArray();
            return data;
        }

        [HttpGet("{id}")]
        public Cookbook GetCookbooks(int id)
        {
            var data = _db.Cookbooks.FirstOrDefault(c => c.Id == id);
            return data;
        }

        [HttpPost]
        public void Post([FromBody] Cookbook cookbook)
        {
            _db.Cookbooks.Add(cookbook);
            _db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteCookbook(int id)
        {
            var data = _db.Cookbooks.FirstOrDefault(c => c.Id == id);
            if (data != null)
            {
                _db.Cookbooks.Remove(data);
                _db.SaveChanges();
            }
        }

        [HttpPut("{id}")]
        public void UpdateCookbook([FromBody] Cookbook cookbook, int id)
        {
            var existingCookbook = _db.Cookbooks.FirstOrDefault(c => c.Id == id);
            if (existingCookbook != null)
            {
                existingCookbook.Title = cookbook.Title;
                existingCookbook.Notes = cookbook.Notes;
            }

            _db.SaveChanges();
        }


        [HttpGet("filtered")]
        public Cookbook[] GetCookbooks([FromQuery] string title)
        {
            var data = _db.Cookbooks.Where(c => c.Title.Contains(title)).ToArray();
            return data;
        }
    
}
}
