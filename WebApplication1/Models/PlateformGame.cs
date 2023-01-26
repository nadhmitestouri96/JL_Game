using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class PlateformGame
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public int GameId { get; set; } 
        [Required]
        public int PlatformId { get; set; }
     
        public Platform Platform { get; set; }
       
        public Game Game { get; set; }  

    }
}
