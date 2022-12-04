using System;
using System.ComponentModel.DataAnnotations;

namespace ContempProgrammingFinal
{
    public class FavoriteGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
    }
}