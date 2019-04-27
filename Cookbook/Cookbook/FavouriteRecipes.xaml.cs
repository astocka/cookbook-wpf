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
    /// Interaction logic for FavouriteRecipes.xaml
    /// </summary>
    public partial class FavouriteRecipes : Page
    {
        public FavouriteRecipes()
        {
            InitializeComponent();
            GetAllFavRecipes();
        }

        public void GetAllFavRecipes()
        {
            try
            {
                var allFavRecipes = DataAccess.GetRecipes();
                var recipeList = new List<string>();

                foreach (var recipe in allFavRecipes)
                {
                    if (recipe.IsFavourite)
                    {
                        recipeList.Add(recipe.Name);
                    }
                }

                lstAllFavRecipes.ItemsSource = recipeList;

            }
            catch (Exception)
            {
                MessageBox.Show("Could not show recipes...");

                var navService = NavigationService.GetNavigationService(this);
                var home = new Home();
                navService?.Navigate(home);
            }
        }

        private void txtSelectedFavRecipe_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var recipeName = lstAllFavRecipes.SelectedItem;
            var recipeId = DataAccess.GetRecipeIdByName(recipeName.ToString());

            var navService = NavigationService.GetNavigationService(this);
            var recipe = new Recipe((int)recipeId);
            navService?.Navigate(recipe);
        }
    }
}
