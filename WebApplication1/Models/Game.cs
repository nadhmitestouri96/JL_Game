using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }

        [JsonIgnore]
        public ICollection<UserGame> UserGames { get; set; }
        [JsonIgnore]
        public ICollection<PlateformGame> PlateformGame { get; set; }

    }
}
