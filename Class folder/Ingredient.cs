using System;

namespace RecipeApp
{
    // This class represents an ingredient in a recipe
    public class Ingredient
    {
        // Properties for the details of an ingredient
        public string Name { get; set; }  // The name of the ingredient
        public double Quantity { get; set; }  // The quantity of the ingredient
        public string Unit { get; set; }  // The unit of measurement for the ingredient
        public int Calories { get; set; }  // The calorie count of the ingredient
        public string FoodGroup { get; set; }  // The food group the ingredient belongs to

        // Constructor to initialize an ingredient with specified details
        public Ingredient(string name, double quantity, string unit, int calories, string foodGroup)
        {
            Name = name;  // Setting the name of the ingredient
            Quantity = quantity;  // Setting the quantity of the ingredient
            Unit = unit;  // Setting the unit of measurement
            Calories = calories;  // Setting the calorie count
            FoodGroup = foodGroup;  // Setting the food group
        }
    }
}       // end of file
