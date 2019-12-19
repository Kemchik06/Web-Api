using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesShopApi.Dtos
{
    public class MovieDto
    {
        public  int Id { get; set; }
        
        public  string Name { get; set; }
        public  double Price { get; set; }
        public string Genre { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte NumberInStock { get; set; }
    }
}
