﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Home();
        }

        private void BtnAddRecipe_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AddRecipe();
        }

        private void BtnAllRecipes_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Recipes();
        }

        private void BtnFavouriteRecipes_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new FavouriteRecipes();
        }
    }
}
