using System;
using System.ComponentModel.DataAnnotations;

namespace ContempProgrammingFinal
{
    public class FavoriteFood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public bool Vegetarian { get; set; }
        public bool EasyToCook { get; set; }
    }
}