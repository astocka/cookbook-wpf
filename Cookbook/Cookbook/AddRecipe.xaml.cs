using System;
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
        public AddRecipe()
        {
            InitializeComponent();
        }

        private void BtnAddRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText =
                        "INSERT INTO Recipes (Name, Ingredients, Preparation) VALUES (@Name, @Ingredients, @Preparation);";
                    command.Prepare();
                    command.Parameters.AddWithValue("@Name", txtRecipeName.Text);
                    command.Parameters.AddWithValue("@Ingredients", txtRecipeIngredients.Text);
                    command.Parameters.AddWithValue("@Preparation", txtRecipePreparation.Text);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Recipe added successfully");

                        var navService = NavigationService.GetNavigationService(this);
                        var home = new Home();
                        navService?.Navigate(home);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Could not add the recipe...");
                    }
                }
            }
        }

        private void BtnCancelAddRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            var navService = NavigationService.GetNavigationService(this);
            var home = new Home();
            navService?.Navigate(home);
        }
    }
}
