using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private RecipeManager recipeManager;

        public MainWindow()
        {
            InitializeComponent();
            recipeManager = new RecipeManager();
            RefreshRecipeList();
        }

        private void RefreshRecipeList()
        {
            lstRecipes.ItemsSource = null;
            lstRecipes.ItemsSource = recipeManager.recipes;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            string ingredientFilter = txtIngredientFilter.Text.Trim().ToLower();
            string foodGroupFilter = (cmbFoodGroupFilter.SelectedItem as ComboBoxItem)?.Content.ToString();
            int maxCalories = 0;

            if (!string.IsNullOrEmpty(txtMaxCalories.Text) && !int.TryParse(txtMaxCalories.Text, out maxCalories))
            {
                MessageBox.Show("Invalid Max Calories value.");
                return;
            }

            var filteredRecipes = recipeManager.recipes.Where(r =>
                (string.IsNullOrEmpty(ingredientFilter) || r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientFilter))) &&
                (foodGroupFilter == "All" || r.Ingredients.Any(i => i.FoodGroup == foodGroupFilter)) &&
                (maxCalories == 0 || r.Ingredients.Sum(i => i.Calories) <= maxCalories)
            );

            lstRecipes.ItemsSource = null;
            lstRecipes.ItemsSource = filteredRecipes;
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(recipeManager);
            if (addRecipeWindow.ShowDialog() == true)
            {
                RefreshRecipeList();
            }
        }

        private void btnViewRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lstRecipes.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                ViewRecipeWindow viewWindow = new ViewRecipeWindow(selectedRecipe);
                viewWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a recipe to view.");
            }
        }

        private void btnUpdateRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = lstRecipes.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                UpdateRecipeWindow updateWindow = new UpdateRecipeWindow(recipeManager, selectedRecipe);
                if (updateWindow.ShowDialog() == true)
                {
                    RefreshRecipeList();
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to update.");
            }
        }

        private void lstRecipes_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            btnViewRecipe_Click(sender, null);
        }
    }
}