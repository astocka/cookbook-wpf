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
    /// Interaction logic for EditRecipe.xaml
    /// </summary>
    public partial class EditRecipe : Page
    {
        public EditRecipe(Data.Recipe recipe)
        {
            InitializeComponent();
            GetRecipe(recipe);
        }

        public void GetRecipe(Data.Recipe recipe)
        {
            try
            {
                txtEditRecipeId.Text = recipe.Id.ToString();
                txtEditRecipeName.Text = recipe.Name;
                txtEditRecipeIngredients.Text = recipe.Ingredients;
                txtEditRecipePreparation.Text = recipe.Ingredients;
                txtEditRecipeIsFavourite.IsChecked = recipe.IsFavourite;
            }
            catch (Exception)
            {
                MessageBox.Show("Load the recipe failed.");
            }
        }

        private void BtnUpdateRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var recipeToUpdate = new Data.Recipe
                {
                    Id = Int32.Parse(txtEditRecipeId.Text),
                    Name = txtEditRecipeName.Text,
                    Ingredients = txtEditRecipeIngredients.Text,
                    Preparation = txtEditRecipePreparation.Text,
                    IsFavourite = txtEditRecipeIsFavourite.IsChecked.Value
                };

                Data.DataAccess.UpdateRecipe(recipeToUpdate);
                MessageBox.Show("The recipe updated successfully.");

                var navService = NavigationService.GetNavigationService(this);
                var recipe = new Recipe(Int32.Parse(txtEditRecipeId.Text));
                navService?.Navigate(recipe);
            }
            catch (Exception)
            {
                MessageBox.Show("Update the recipe failed.");
            }
        }

        private void BtnCancelUpdateRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            var navService = NavigationService.GetNavigationService(this);
            var recipe = new Recipe(Int32.Parse(txtEditRecipeId.Text));
            navService?.Navigate(recipe);
        }
    }
}
