﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Cookbook.Data
{
    public static class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SQLiteConnection conn =
                new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                String recipeTableQuery = "CREATE TABLE IF NOT " +
                                            "EXISTS Recipes (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                                            "Name TEXT NOT NULL, " +
                                            "Ingredients TEXT NOT NULL," +
                                            "Preparation TEXT NOT NULL)";
                SQLiteCommand createTableCommand = new SQLiteCommand(recipeTableQuery, conn);
                createTableCommand.ExecuteReader();
            }
        }

        public static void AddRecipe(Recipe recipe)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText =
                        "INSERT INTO Recipes (Name, Ingredients, Preparation) VALUES (@Name, @Ingredients, @Preparation);";
                    command.Prepare();
                    command.Parameters.AddWithValue("@Name", recipe.Name);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Preparation", recipe.Preparation);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}