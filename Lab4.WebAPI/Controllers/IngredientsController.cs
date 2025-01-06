using Lab2.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private RecipeDbContext _db;

        public IngredientsController()
        {
            _db = new RecipeDbContext();
        }

        [HttpGet]
        public Ingredient[] GetIngredients()
        {
            var data = _db.Ingredients.ToArray();
            return data;
        }

        [HttpGet("{id}")]
        public Ingredient GetIngredients(int id)
        {
            var data = _db.Ingredients.FirstOrDefault(i => i.Id == id);
            return data;
        }

        [HttpPost]
        public void Post([FromBody] Ingredient ingredient)
        {
            _db.Ingredients.Add(ingredient);
            _db.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteIngredient(int id)
        {
            var data = _db.Ingredients.FirstOrDefault(c => c.Id == id);
            if (data != null)
            {
                _db.Ingredients.Remove(data);
                _db.SaveChanges();
            }
        }

        [HttpPut("{id}")]
        public void UpdateIngredient([FromBody] Ingredient ingredient, int id)
        {
            var existingIngredient = _db.Ingredients.FirstOrDefault(c => c.Id == id);
            if (existingIngredient != null)
            {
                existingIngredient.Name = ingredient.Name;
                existingIngredient.Amount = ingredient.Amount;
                existingIngredient.Units = ingredient.Units;
            }

            _db.SaveChanges();
        }
    }
}
