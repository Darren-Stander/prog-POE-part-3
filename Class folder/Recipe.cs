using System.Collections.Generic;  // Importing the necessary libraries for collections
using System.Linq;  // Importing LINQ for querying collections

namespace RecipeApp
{
    // This class represents a recipe, including its name, ingredients, and steps
    public class Recipe
    {
        // Private fields to hold the ingredients and steps of the recipe
        private List<Ingredient> ingredients;
        private List<RecipeStep> steps;

        // Constructor to initialize the recipe with a name
        public Recipe(string name)
        {
            Name = name;  // Setting the name of the recipe
            ingredients = new List<Ingredient>();  // Initializing the list of ingredients
            steps = new List<RecipeStep>();  // Initializing the list of steps
        }

        // Public property to get or set the name of the recipe
        public string Name { get; set; }

        // Public property to get or set the list of ingredients
        public List<Ingredient> Ingredients
        {
            get => ingredients;  // Returning the list of ingredients
            set => ingredients = value;  // Setting the list of ingredients
        }

        // Public property to get or set the list of steps
        public List<RecipeStep> Steps
        {
            get => steps;  // Returning the list of steps
            set => steps = value;  // Setting the list of steps
        }

        // Public property to get a summary of the ingredients
        public string IngredientSummary
        {
            get
            {
                if (Ingredients == null || Ingredients.Count == 0) return "";  // Return an empty string if there are no ingredients
                return string.Join(", ", Ingredients.Select(i => $"{i.Quantity} {i.Unit} {i.Name}"));  // Joining the ingredients into a single string
            }
        }

        // Public property to get a summary of the food groups of the ingredients
        public string FoodGroupSummary
        {
            get
            {
                if (Ingredients == null || Ingredients.Count == 0) return "";  // Return an empty string if there are no ingredients
                return string.Join(", ", Ingredients.Select(i => i.FoodGroup).Distinct());  // Joining distinct food groups into a single string
            }
        }

        // Public property to get the total calories of the recipe
        public int TotalCalories
        {
            get { return Ingredients.Sum(i => i.Calories); }  // Summing up the calories of all ingredients
        }
    }
}
