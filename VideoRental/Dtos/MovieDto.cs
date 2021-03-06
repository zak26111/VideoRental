using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoRental.Dtos;

namespace VideoRental.Models
{
    public class MovieDto
    {
        public int Id { get; set; }       
        public string Name { get; set; }
        public GenreDto Genre { get; set; }        
        public byte GenreId { get; set; }     
        public DateTime? DateAdded { get; set; }      
        public DateTime? ReleaseDate { get; set; }      
        public int NumberInStock { get; set; }
    }
}