using System;  // Importing System namespace for basic functionality
using System.Collections.Generic;  // Importing namespace for collections
using System.Linq;  // Importing LINQ for querying collections
using System.Windows;  // Importing namespace for Windows Presentation Foundation (WPF) core functionality
using System.Windows.Controls;  // Importing namespace for WPF controls

namespace RecipeApp
{
    // This class represents the window for updating an existing recipe
    public partial class UpdateRecipeWindow : Window
    {
        // Private fields for managing recipes, the specific recipe to update, ingredients, and steps
        private RecipeManager recipeManager;
        private Recipe recipeToUpdate;
        private List<Ingredient> ingredients;
        private List<RecipeStep> steps;

        // Constructor for the UpdateRecipeWindow class
        public UpdateRecipeWindow(RecipeManager recipeManager, Recipe recipe)
        {
            InitializeComponent();  // Initializing the components defined in XAML
            this.recipeManager = recipeManager;  // Setting the RecipeManager instance
            this.recipeToUpdate = recipe;  // Setting the specific recipe to update

            // Initializing the ingredients and steps with the recipe's existing data
            ingredients = recipe.Ingredients;
            steps = recipe.Steps;

            // Loading the recipe data into the input fields
            txtName.Text = recipe.Name;

            // Loading the existing ingredients into the UI
            foreach (var ingredient in recipe.Ingredients)
            {
                AddIngredientToPanel(ingredient);
            }

            // Setting the item source for the list of steps
            lstSteps.ItemsSource = steps;
        }

        // Event handler for adding a new ingredient
        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Adding a new default ingredient to the panel
            AddIngredientToPanel(new Ingredient("Name", 1, "", 0, "Grains"));
        }

        // Method for adding an ingredient to the UI panel
        private void AddIngredientToPanel(Ingredient ingredient)
        {
            // Creating a new StackPanel to hold the ingredient input controls
            StackPanel ingredientPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };

            // Creating and initializing input controls for the ingredient
            TextBox nameBox = new TextBox() { Width = 100, Margin = new Thickness(5), Text = ingredient.Name };
            TextBox qtyBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = ingredient.Quantity.ToString() };
            ComboBox unitComboBox = new ComboBox() { Width = 60, Margin = new Thickness(5), SelectedItem = ingredient.Unit };
            TextBox calsBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = ingredient.Calories.ToString() };
            ComboBox foodGroupComboBox = new ComboBox() { Width = 100, Margin = new Thickness(5), SelectedItem = ingredient.FoodGroup };

            // Adding unit options to the unit ComboBox (in alphabetical order)
            unitComboBox.Items.Add("cl");
            unitComboBox.Items.Add("cup");
            unitComboBox.Items.Add("dl");
            unitComboBox.Items.Add("fl oz");
            unitComboBox.Items.Add("grams");
            unitComboBox.Items.Add("hg");
            unitComboBox.Items.Add("kg");
            unitComboBox.Items.Add("l");
            unitComboBox.Items.Add("mg");
            unitComboBox.Items.Add("ml");
            unitComboBox.Items.Add("oz");
            unitComboBox.Items.Add("t");  // metric tons
            unitComboBox.Items.Add("tbsp");
            unitComboBox.Items.Add("tsp");
            unitComboBox.Items.Add("μl");
            unitComboBox.Items.Add("");  // Empty option

            // Adding food group options to the food group ComboBox (in alphabetical order)
            foodGroupComboBox.Items.Add("Dairy");
            foodGroupComboBox.Items.Add("Fruits");
            foodGroupComboBox.Items.Add("Grains");
            foodGroupComboBox.Items.Add("Protein");
            foodGroupComboBox.Items.Add("Vegetables");

            // Adding the input controls to the ingredient panel
            ingredientPanel.Children.Add(nameBox);
            ingredientPanel.Children.Add(qtyBox);
            ingredientPanel.Children.Add(unitComboBox);
            ingredientPanel.Children.Add(calsBox);
            ingredientPanel.Children.Add(foodGroupComboBox);

            // Adding the ingredient panel to the panel for displaying ingredients
            pnlIngredients.Children.Add(ingredientPanel);
        }

        // Event handler for saving the updated recipe
        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            // 1. Update the recipeToUpdate object with the data from the input fields
            recipeToUpdate.Name = txtName.Text;  // Updating the recipe name

            // Clearing the existing ingredients and updating with the new data
            recipeToUpdate.Ingredients.Clear();
            foreach (StackPanel ingredientPanel in pnlIngredients.Children.OfType<StackPanel>())
            {
                if (ingredientPanel.Children.Count == 5 &&
                    ingredientPanel.Children[0] is TextBox nameBox &&
                    ingredientPanel.Children[1] is TextBox qtyBox &&
                    ingredientPanel.Children[2] is ComboBox unitComboBox &&
                    ingredientPanel.Children[3] is TextBox calsBox &&
                    ingredientPanel.Children[4] is ComboBox foodGroupBox)
                {
                    string name = nameBox.Text.Trim();
                    if (string.IsNullOrEmpty(name)) continue;  // Skip if the name is empty

                    // Parsing the quantity and calories inputs
                    if (!double.TryParse(qtyBox.Text.Trim(), out double quantity) ||
                        !int.TryParse(calsBox.Text.Trim(), out int calories))
                    {
                        MessageBox.Show("Invalid ingredient quantity or calories.");  // Showing an error message if parsing fails
                        return;
                    }

                    string unit = unitComboBox.SelectedItem?.ToString() ?? "";  // Getting the selected unit
                    string foodGroup = foodGroupBox.SelectedItem?.ToString() ?? "";  // Getting the selected food group

                    // Adding the ingredient to the list of ingredients
                    recipeToUpdate.Ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
                }
            }

            // Updating the steps with the new data from the UI
            recipeToUpdate.Steps = lstSteps.Items.OfType<RecipeStep>().ToList();

            this.DialogResult = true;  // Setting the dialog result to true to indicate success
        }

        // Event handler for adding a new step to the recipe
        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            steps.Add(new RecipeStep { Description = "New Step", IsCompleted = false });  // Adding a new step with default values
            lstSteps.Items.Refresh();  // Refreshing the list of steps displayed in the UI
        }

        // Event handler for deleting the recipe
        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Confirmation dialog
            if (MessageBox.Show("Are you sure you want to delete this recipe?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Removing the recipe from the recipeManager's list
                recipeManager.recipes.Remove(recipeToUpdate);
                this.DialogResult = true;  // Indicate that the recipe was deleted
                this.Close();  // Close the window
            }
        }
    }
}

// End of file
