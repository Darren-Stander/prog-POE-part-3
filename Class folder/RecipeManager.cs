using System.Collections.Generic;  // Importing namespace for collections

namespace RecipeApp
{
    // This class is responsible for managing a collection of recipes
    public class RecipeManager
    {
        // Public field to hold the list of recipes
        public List<Recipe> recipes;

        // Constructor for the RecipeManager class
        public RecipeManager()
        {
            recipes = new List<Recipe>();  // Initializing the list of recipes
        }
    }
}

// End of file
