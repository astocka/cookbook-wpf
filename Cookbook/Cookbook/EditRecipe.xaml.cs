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
        List<string> editIngredients = new List<string>();
        List<string> ingredientsToUpdate = new List<string>();

        public EditRecipe(Data.Recipe recipe)
        {
            InitializeComponent();
            GetRecipe(recipe);

        }

        private List<string> GetListIngredients(string ingredientsToConvert)
        {
            List<string> listIngredients = new List<string>();
            string[] changed = ingredientsToConvert.Split(';');
            foreach (var ingredient in changed)
            {
                listIngredients.Add(ingredient);
            }
            return listIngredients;
        }

        private string GetIngredientsToUpdate(List<string> input)
        {
            string delimiter = ";";
            string result = string.Join(delimiter, input);
            return result;
        }

        public void GetRecipe(Data.Recipe recipe)
        {
            try
            {
                txtEditRecipeId.Text = recipe.Id.ToString();
                txtEditRecipeName.Text = recipe.Name;
                lstIngredientsToEdit.ItemsSource = GetListIngredients(recipe.Ingredients);
                txtEditRecipePreparation.Text = recipe.Preparation;
                txtEditRecipeIsFavourite.IsChecked = recipe.IsFavourite;

                editIngredients = GetListIngredients(recipe.Ingredients);
                ingredientsToUpdate = editIngredients;
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
                    Ingredients = GetIngredientsToUpdate(ingredientsToUpdate),
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

        private void BtnAddNewIngredient_OnClick(object sender, RoutedEventArgs e)
        {
            ingredientsToUpdate.Add(txtEditRecipeIngredient.Text.Trim());
            lstIngredientsToEdit.ItemsSource = ingredientsToUpdate;
            txtEditRecipeIngredient.Text = "";
        }

        private void BtnDeleteIngredient_OnClick(object sender, RoutedEventArgs e)
        {
            ingredientsToUpdate.Remove(lstIngredientsToEdit.SelectedItem.ToString());
            lstIngredientsToEdit.ItemsSource = "";
            lstIngredientsToEdit.ItemsSource = ingredientsToUpdate;
        }
    }
}
