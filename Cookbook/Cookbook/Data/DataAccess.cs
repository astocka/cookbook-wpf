using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Diagnostics;
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
                                            "Preparation TEXT NOT NULL," +
                                            "IsFavourite INTEGER NOT NULL DEFAULT 0)";
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
                        "INSERT INTO Recipes (Name, Ingredients, Preparation, IsFavourite) VALUES (@Name, @Ingredients, @Preparation, @IsFavourite);";
                    command.Prepare();
                    command.Parameters.AddWithValue("@Name", recipe.Name);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Preparation", recipe.Preparation);
                    command.Parameters.AddWithValue("@IsFavourite", recipe.IsFavourite);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Recipe> GetRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "SELECT * FROM Recipes;";
                    var query = command.ExecuteReader();

                    while (query.Read())
                    {
                        recipes.Add(new Recipe(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetString(3), query.GetBoolean(4)));
                    }
                }
            }
            return recipes;
        }

        public static Recipe GetRecipeById(int id)
        {
            Recipe recipe = new Recipe();

            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "SELECT * FROM Recipes WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@Id", id);

                    var query = command.ExecuteReader();

                    while (query.Read())
                    {
                        recipe.Id = query.GetInt32(0);
                        recipe.Name = query.GetString(1);
                        recipe.Ingredients = query.GetString(2);
                        recipe.Preparation = query.GetString(3);
                        recipe.IsFavourite = query.GetBoolean(4);
                    }
                }
            }
            return recipe;
        }

        public static int GetRecipeIdByName(string name)
        {
            var recipeId = -1;

            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "SELECT Id FROM Recipes WHERE Name = @Name;";
                    command.Parameters.AddWithValue("@Name", name);

                    var query = command.ExecuteReader();

                    while (query.Read())
                    {
                        recipeId = query.GetInt32(0);
                    }
                }
            }
            return recipeId;
        }

        public static void AddToFavRecipe(Recipe recipe)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText =
                        "UPDATE Recipes SET IsFavourite = 1 WHERE Id = @Id;";
                    command.Prepare();
                    command.Parameters.AddWithValue("@Id", recipe.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateRecipe(Recipe recipe)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText =
                        "UPDATE Recipes SET Name = @Name, Ingredients = @Ingredients, Preparation = @Preparation, IsFavourite = @IsFavourite WHERE Id = @Id;";
                    command.Prepare();
                    command.Parameters.AddWithValue("@Id", recipe.Id);
                    command.Parameters.AddWithValue("@Name", recipe.Name);
                    command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                    command.Parameters.AddWithValue("@Preparation", recipe.Preparation);
                    command.Parameters.AddWithValue("@IsFavourite", recipe.IsFavourite);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteRecipe(Recipe recipe)
        {
            using (SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "DELETE FROM Recipes WHERE Id = @Id;";
                    command.Prepare();
                    command.Parameters.AddWithValue("@Id", recipe.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
