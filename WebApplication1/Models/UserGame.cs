using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class UserGame
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int GameId { get; set; }
        [Required]

        public int UserId { get; set; }
        [Required]

        public int TotalPlayTime { get; set; }
       
        public User User { get; set; }
      
        public Game Game { get; set; }  

    }
}
