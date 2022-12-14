using System;
using System.ComponentModel.DataAnnotations;

namespace ContempProgrammingFinal
{
    public class FavoriteMovie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string LeadActor { get; set; }
        public bool OnNetflix { get; set; }
    }
}