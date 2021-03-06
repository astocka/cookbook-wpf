﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
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
    /// Interaction logic for AddRecipe.xaml
    /// </summary>
    public partial class AddRecipe : Page
    {
        List<string> ingredients = new List<string>();

        public AddRecipe()
        {
            InitializeComponent();
        }

        private string GetIngredients(List<string> input)
        {
            string delimiter = ";";
            string result =  string.Join(delimiter, input);
            return result;
        }

        private void BtnAddRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            Data.Recipe recipe = new Data.Recipe
            {
                Name = txtRecipeName.Text,
                Ingredients = GetIngredients(ingredients),
                Preparation = txtRecipePreparation.Text,
                IsFavourite = txtRecipeIsFavourite.IsChecked.Value
            };
            
            try
            {
                DataAccess.AddRecipe(recipe);
                MessageBox.Show("Recipe added successfully");

                var navService = NavigationService.GetNavigationService(this);
                var recipes = new Recipes();
                navService?.Navigate(recipes);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not add the recipe...");
            }
        }

        private void BtnCancelAddRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            var navService = NavigationService.GetNavigationService(this);
            var recipes = new Recipes();
            navService?.Navigate(recipes);
        }

        private void BtnAddIngredient_OnClick(object sender, RoutedEventArgs e)
        {
            ingredients.Add(txtRecipeIngredient.Text.Trim());
            lstIngredientsToAdd.Items.Add(txtRecipeIngredient.Text);
            txtRecipeIngredient.Text = "";
        }
    }
}
