using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class AddRecipeWindow : Window
    {
        private RecipeManager recipeManager;
        private List<Ingredient> ingredients;
        private List<RecipeStep> steps;

        public AddRecipeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            this.recipeManager = recipeManager;
            ingredients = new List<Ingredient>();
            steps = new List<RecipeStep>();
            lstSteps.ItemsSource = steps;
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            StackPanel ingredientPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(5) };

            TextBox nameBox = new TextBox() { Width = 100, Margin = new Thickness(5), Text = "Name" };
            TextBox qtyBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = "Qty" };
            ComboBox unitComboBox = new ComboBox() { Width = 60, Margin = new Thickness(5) };
            TextBox calsBox = new TextBox() { Width = 50, Margin = new Thickness(5), Text = "Cals" };
            ComboBox foodGroupComboBox = new ComboBox() { Width = 100, Margin = new Thickness(5) };

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
            string recipeName = txtName.Text.Trim();
            if (string.IsNullOrEmpty(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.");
                return;
            }

            ingredients.Clear();
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

                    ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
                }
            }

            List<RecipeStep> newSteps = lstSteps.Items.OfType<RecipeStep>().ToList();

            Recipe newRecipe = new Recipe(recipeName)
            {
                Ingredients = ingredients,
                Steps = newSteps
            };

            recipeManager.recipes.Add(newRecipe);
            this.DialogResult = true;
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            steps.Add(new RecipeStep { Description = "New Step", IsCompleted = false });
            lstSteps.Items.Refresh();
        }
    }
}