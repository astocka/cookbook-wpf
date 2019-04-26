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
                txtReadRecipeName.Text = recipe.Name;
                txtReadRecipeIngredients.Text = recipe.Ingredients;
                txtReadRecipePreparation.Text = recipe.Preparation;
            }
            catch (Exception)
            {
                MessageBox.Show("Show recipe details failed.");
            }
        }

        private void BtnAddToFavRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Added to favourite");
        }

        private void BtnEditRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit the recipe.");
        }

        private void BtnDeleteRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure you want to delete the recipe?");
        }
    }
}
