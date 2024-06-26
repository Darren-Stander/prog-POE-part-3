using System;  // Importing System namespace for basic functionality
using System.Collections.Generic;  // Importing namespace for collections
using System.Linq;  // Importing LINQ for querying collections
using System.Windows;  // Importing namespace for Windows Presentation Foundation (WPF) core functionality
using System.Windows.Controls;  // Importing namespace for WPF controls

namespace RecipeApp
{
    // This class represents the window for adding a new recipe
    public partial class AddRecipeWindow : Window
    {
        // Private fields for managing recipes, ingredients, and steps
        private RecipeManager recipeManager;
        private List<Ingredient> ingredients;
        private List<RecipeStep> steps;

        // Constructor for the AddRecipeWindow class
        public AddRecipeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();  // Initializing the components defined in XAML
            this.recipeManager = recipeManager;  // Setting the RecipeManager instance
            ingredients = new List<Ingredient>();  // Initializing the list of ingredients
            steps = new List<RecipeStep>();  // Initializing the list of steps
            lstSteps.ItemsSource = steps;  // Setting the item source for the list of steps
        }

        // Event handler for adding a new ingredient
        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Creating a new StackPanel to hold the ingredient input controls
            StackPanel ingredientPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };

            // Creating and initializing input controls for the ingredient
            TextBox nameBox = new TextBox() { Width = 100, Margin = new Thickness(5), Text = "Name" };
            TextBox qtyBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = "Qty" };
            ComboBox unitComboBox = new ComboBox() { Width = 60, Margin = new Thickness(5) };
            TextBox calsBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = "Cals" };
            ComboBox foodGroupComboBox = new ComboBox() { Width = 100, Margin = new Thickness(5) };

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

        // Event handler for saving the new recipe
        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = txtName.Text.Trim();  // Getting the recipe name from the input field
            if (string.IsNullOrEmpty(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.");  // Showing an error message if the recipe name is empty
                return;
            }

            ingredients.Clear();  // Clearing the current list of ingredients
            // Iterating through the ingredient panels and extracting the input values
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
                    ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
                }
            }

            // Getting the list of steps from the UI
            List<RecipeStep> newSteps = lstSteps.Items.OfType<RecipeStep>().ToList();

            // Creating a new Recipe object with the input details
            Recipe newRecipe = new Recipe(recipeName)
            {
                Ingredients = ingredients,
                Steps = newSteps
            };

            // Adding the new recipe to the RecipeManager
            recipeManager.recipes.Add(newRecipe);
            this.DialogResult = true;  // Setting the dialog result to true to indicate success
        }

        // Event handler for adding a new step to the recipe
        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            steps.Add(new RecipeStep { Description = "New Step", IsCompleted = false });  // Adding a new step with default values
            lstSteps.Items.Refresh();  // Refreshing the list of steps displayed in the UI
        }
    }
}

// End of file
