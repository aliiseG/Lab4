using Lab2.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Lab4.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private RecipeDbContext _db;

        public RecipesController()
        {
            _db = new RecipeDbContext();
        }

        [HttpGet]
        public Recipe[] GetRecipes()
        {
            var data = _db.Recipes.ToArray();
            return data;
        }

        [HttpGet("{id}")]
        public Recipe GetRecipe(int id)
        {
            var data = _db.Recipes.FirstOrDefault(r => r.Id == id);
            return data;
        }

        [HttpPost]
        public void Post([FromBody] Recipe recipe)
        {
            _db.Recipes.Add(recipe);
            _db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteRecipe(int id)
        {
            var data = _db.Recipes.FirstOrDefault(r => r.Id == id);
            if (data != null) {
                _db.Recipes.Remove(data);
                _db.SaveChanges();
            }
        }

        [HttpPut("{id}")]
        public void UpdateRecipe([FromBody] Recipe recipe, int id)
        {
            var existingRecipe = _db.Recipes.FirstOrDefault(r => r.Id == id);
            if (existingRecipe != null)
            {
                existingRecipe.DishName = recipe.DishName;
                existingRecipe.Category = recipe.Category;
                existingRecipe.PrepTime = recipe.PrepTime;
            }

            _db.SaveChanges();
        }
        [HttpGet("filtered")]
        public Recipe[] GetRecipes([FromQuery] int max_preptime)
        {
            var data = _db.Recipes.Where(r => r.PrepTime <= max_preptime).ToArray();
            return data;
        }


    }

}
