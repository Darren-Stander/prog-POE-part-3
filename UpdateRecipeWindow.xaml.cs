using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class UpdateRecipeWindow : Window
    {
        private RecipeManager recipeManager;
        private Recipe recipeToUpdate;
        private List<Ingredient> ingredients;
        private List<RecipeStep> steps;

        public UpdateRecipeWindow(RecipeManager recipeManager, Recipe recipe)
        {
            InitializeComponent();
            this.recipeManager = recipeManager;
            this.recipeToUpdate = recipe;

            ingredients = recipe.Ingredients;
            steps = recipe.Steps;

            // Load recipe data into the input fields
            txtName.Text = recipe.Name;

            // Load ingredients
            foreach (var ingredient in recipe.Ingredients)
            {
                AddIngredientToPanel(ingredient);
            }

            // Load steps
            lstSteps.ItemsSource = steps;
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientToPanel(new Ingredient("Name", 1, "", 0, "Grains"));
        }

        private void AddIngredientToPanel(Ingredient ingredient)
        {
            StackPanel ingredientPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };

            TextBox nameBox = new TextBox() { Width = 100, Margin = new Thickness(5), Text = ingredient.Name };
            TextBox qtyBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = ingredient.Quantity.ToString() };
            ComboBox unitComboBox = new ComboBox() { Width = 60, Margin = new Thickness(5), SelectedItem = ingredient.Unit };
            TextBox calsBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = ingredient.Calories.ToString() };
            ComboBox foodGroupComboBox = new ComboBox() { Width = 100, Margin = new Thickness(5), SelectedItem = ingredient.FoodGroup };

            // Add unit options (in alphabetical order)
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
            unitComboBox.Items.Add("t"); // metric tons
            unitComboBox.Items.Add("tbsp");
            unitComboBox.Items.Add("tsp");
            unitComboBox.Items.Add("μl");
            unitComboBox.Items.Add(""); // Empty option

            // Add food group options (in alphabetical order)
            foodGroupComboBox.Items.Add("Dairy");
            foodGroupComboBox.Items.Add("Fruits");
            foodGroupComboBox.Items.Add("Grains");
            foodGroupComboBox.Items.Add("Protein");
            foodGroupComboBox.Items.Add("Vegetables");

            ingredientPanel.Children.Add(nameBox);
            ingredientPanel.Children.Add(qtyBox);
            ingredientPanel.Children.Add(unitComboBox);
            ingredientPanel.Children.Add(calsBox);
            ingredientPanel.Children.Add(foodGroupComboBox);

            pnlIngredients.Children.Add(ingredientPanel);
        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            // 1. Update the recipeToUpdate object with the data from the input fields
            recipeToUpdate.Name = txtName.Text;

            // Update ingredients
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
                    if (string.IsNullOrEmpty(name)) continue;

                    if (!double.TryParse(qtyBox.Text.Trim(), out double quantity) ||
                        !int.TryParse(calsBox.Text.Trim(), out int calories))
                    {
                        MessageBox.Show("Invalid ingredient quantity or calories.");
                        return;
                    }

                    string unit = unitComboBox.SelectedItem?.ToString() ?? "";
                    string foodGroup = foodGroupBox.SelectedItem?.ToString() ?? "";

                    recipeToUpdate.Ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
                }
            }

            // Update steps
            recipeToUpdate.Steps = lstSteps.Items.OfType<RecipeStep>().ToList();

            this.DialogResult = true;
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            steps.Add(new RecipeStep { Description = "New Step", IsCompleted = false });
            lstSteps.Items.Refresh();
        }

        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Confirmation dialog
            if (MessageBox.Show("Are you sure you want to delete this recipe?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Remove the recipe from the recipeManager's list
                recipeManager.recipes.Remove(recipeToUpdate);
                this.DialogResult = true; // Indicate that the recipe was deleted
                this.Close(); // Close the window
            }
        }
    }
}