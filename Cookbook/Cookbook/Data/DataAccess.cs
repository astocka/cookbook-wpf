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
                        recipes.Add(new Recipe(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetString(3)));
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
    }
}
