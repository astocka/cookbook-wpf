using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public bool IsFavourite { get; set; }

        public Recipe(int id, string name, string ingredients, string preparation, bool isFavourite)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
            Preparation = preparation;
            IsFavourite = isFavourite;
        }

        public Recipe()
        {
        }

    }
}
