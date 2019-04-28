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
    /// Interaction logic for RecipeToPrint.xaml
    /// </summary>
    public partial class RecipeToPrint : Page
    {
        public RecipeToPrint(Data.Recipe recipe)
        {
            InitializeComponent();
            PrintDocument(recipe);
        }

        private void PrintDocument(Data.Recipe recipe)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                try
                {
                    printName.Text = recipe.Name;
                    printIngredients.ItemsSource = recipe.Ingredients.Split(';').ToList();
                    printPreparation.Text = recipe.Preparation;

                    printDialog.PrintDocument(((IDocumentPaginatorSource)recipeToPrint).DocumentPaginator, recipe.Name);
                }
                catch (Exception)
                {
                    MessageBox.Show("Print failed.");
                }
            }
        }
    }
}
