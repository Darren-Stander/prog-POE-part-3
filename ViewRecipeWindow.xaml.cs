using System.Windows;

namespace RecipeApp
{
    public partial class ViewRecipeWindow : Window
    {
        public ViewRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            txtRecipeName.Text = recipe.Name;
            lstIngredients.ItemsSource = recipe.Ingredients;
            lstSteps.ItemsSource = recipe.Steps;
        }
    }
}

