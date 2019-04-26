using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cookbook.Data;

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for Recipes.xaml
    /// </summary>
    public partial class Recipes : Page
    {
        public Recipes()
        {
            InitializeComponent();
            GetAllRecipes();
        }

        public void GetAllRecipes()
        {
            try
            {
                var allRecipes = DataAccess.GetRecipes();
                var recipeList = new List<string>();

                foreach (var recipe in allRecipes)
                {
                    recipeList.Add(recipe.Name);
                }

                lstAllRecipes.ItemsSource = recipeList;

            }
            catch (Exception)
            {
                MessageBox.Show("Could not show recipes...");

                var navService = NavigationService.GetNavigationService(this);
                var home = new Home();
                navService?.Navigate(home);
            }
        }

        private void txtSelectedRecipe_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var recipeName = lstAllRecipes.SelectedItem;
            var recipeId = DataAccess.GetRecipeIdByName(recipeName.ToString());
            var navService = NavigationService.GetNavigationService(this);
            var recipe = new Recipe((int)recipeId);
            navService?.Navigate(recipe);
        }
    }
}
