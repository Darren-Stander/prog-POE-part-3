using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{
    public class Recipe
    {
        private List<Ingredient> ingredients;
        private List<RecipeStep> steps;

        public Recipe(string name)
        {
            Name = name;
            ingredients = new List<Ingredient>();
            steps = new List<RecipeStep>();
        }

        public string Name { get; set; }

        public List<Ingredient> Ingredients
        {
            get => ingredients;
            set => ingredients = value;
        }

        public List<RecipeStep> Steps
        {
            get => steps;
            set => steps = value;
        }

        public string IngredientSummary
        {
            get
            {
                if (Ingredients == null || Ingredients.Count == 0) return "";
                return string.Join(", ", Ingredients.Select(i => $"{i.Quantity} {i.Unit} {i.Name}"));
            }
        }

        public string FoodGroupSummary
        {
            get
            {
                if (Ingredients == null || Ingredients.Count == 0) return "";
                return string.Join(", ", Ingredients.Select(i => i.FoodGroup).Distinct());
            }
        }

        public int TotalCalories
        {
            get { return Ingredients.Sum(i => i.Calories); }
        }
    }
}