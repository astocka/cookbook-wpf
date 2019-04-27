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

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for Recipe.xaml
    /// </summary>
    public partial class Recipe : Page
    {
        public Recipe(int id)
        {
            InitializeComponent();
            GetRecipe(id);
        }

        private void GetRecipe(int id)
        {
            try
            {
                var recipe = Data.DataAccess.GetRecipeById(id);
                txtReadRecipeId.Text = recipe.Id.ToString();
                txtReadRecipeName.Text = recipe.Name;
                txtReadRecipeIngredients.Text = recipe.Ingredients;
                txtReadRecipePreparation.Text = recipe.Preparation;

                if (recipe.IsFavourite)
                {
                    imgFavRecipe.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Show recipe details failed.");
            }
        }

        private void BtnAddToFavRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var recipe = Data.DataAccess.GetRecipeById(Int32.Parse(txtReadRecipeId.Text));
                if (!recipe.IsFavourite)
                {
                    Data.DataAccess.AddToFavRecipe(recipe);
                    MessageBox.Show("Recipe added to favourite");
                    imgFavRecipe.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("The recipe is already in favourite!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Add recipe to fav failed.");
            }
        }

        private void BtnEditRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Recipe recipe = Data.DataAccess.GetRecipeById(Int32.Parse(txtReadRecipeId.Text));

                var navService = NavigationService.GetNavigationService(this);
                var editRecipe = new EditRecipe(recipe);
                navService?.Navigate(editRecipe);
            }
            catch (Exception)
            {
                MessageBox.Show("Edit recipe failed.");
            }
        }

        private void BtnDeleteRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure you want to delete the recipe?");
        }
    }
}
