using System;  // Importing System namespace for basic functionality
using System.Collections.Generic;  // Importing namespace for collections
using System.Linq;  // Importing LINQ for querying collections
using System.Windows;  // Importing namespace for Windows Presentation Foundation (WPF) core functionality
using System.Windows.Controls;  // Importing namespace for WPF controls

namespace RecipeApp
{
    // This class represents the main window of the RecipeApp
    public partial class MainWindow : Window
    {
        // Private field to manage recipes
        private RecipeManager recipeManager;

        // Constructor for the MainWindow class
        public MainWindow()
        {
            InitializeComponent();  // Initializing the components defined in XAML
            recipeManager = new RecipeManager();  // Initializing the RecipeManager
            RefreshRecipeList();  // Refreshing the list of recipes displayed in the UI
        }

        // Method to refresh the list of recipes displayed in the UI
        private void RefreshRecipeList()
        {
            lstRecipes.ItemsSource = null;  // Clearing the current items in the list
            lstRecipes.ItemsSource = recipeManager.recipes;  // Setting the item source to the current list of recipes
        }

        // Event handler for the Apply Filter button click event
        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            // Getting the ingredient filter text and converting it to lowercase
            string ingredientFilter = txtIngredientFilter.Text.Trim().ToLower();
            // Getting the selected food group filter
            string foodGroupFilter = (cmbFoodGroupFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            int maxCalories = 0;

            // Validating and parsing the max calories input
            if (!string.IsNullOrEmpty(txtMaxCalories.Text) && !int.TryParse(txtMaxCalories.Text, out maxCalories))
            {
                MessageBox.Show("Invalid Max Calories value.");  // Showing an error message if the input is invalid
                return;
            }

            // Filtering the recipes based on the input filters
            var filteredRecipes = recipeManager.recipes.Where(r =>
                (string.IsNullOrEmpty(ingredientFilter) || r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientFilter))) &&
                (foodGroupFilter == "All" || r.Ingredients.Any(i => i.FoodGroup == foodGroupFilter)) &&
                (maxCalories == 0 || r.Ingredients.Sum(i => i.Calories) <= maxCalories)
            );

            // Updating the list of recipes displayed in the UI with the filtered results
            lstRecipes.ItemsSource = null;
            lstRecipes.ItemsSource = filteredRecipes;
        }

        // Event handler for the Add Recipe button click event
        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(recipeManager);  // Creating a new instance of the AddRecipeWindow
            if (addRecipeWindow.ShowDialog() == true)  // Showing the AddRecipeWindow as a dialog and checking if the dialog result is true
            {
                RefreshRecipeList();  // Refreshing the list of recipes if a new recipe was added
            }
        }

        // Event handler for the View Recipe button click event
        private void btnViewRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lstRecipes.SelectedItem as Recipe;  // Getting the selected recipe from the list
            if (selectedRecipe != null)
            {
                ViewRecipeWindow viewWindow = new ViewRecipeWindow(selectedRecipe);  // Creating a new instance of the ViewRecipeWindow
                viewWindow.ShowDialog();  // Showing the ViewRecipeWindow as a dialog
            }
            else
            {
                MessageBox.Show("Please select a recipe to view.");  // Showing an error message if no recipe is selected
            }
        }

        // Event handler for the Update Recipe button click event
        private void btnUpdateRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lstRecipes.SelectedItem as Recipe;  // Getting the selected recipe from the list
            if (selectedRecipe != null)
            {
                UpdateRecipeWindow updateWindow = new UpdateRecipeWindow(recipeManager, selectedRecipe);  // Creating a new instance of the UpdateRecipeWindow
                if (updateWindow.ShowDialog() == true)  // Showing the UpdateRecipeWindow as a dialog and checking if the dialog result is true
                {
                    RefreshRecipeList();  // Refreshing the list of recipes if the recipe was updated
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to update.");  // Showing an error message if no recipe is selected
            }
        }

        // Event handler for the double-click event on the list of recipes
        private void lstRecipes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnViewRecipe_Click(sender, null);  // Calling the View Recipe button click event handler
        }
    }
}

// End of file
