namespace WebApplication1.ModelView
{
    public class GameResponse
    {
        public string Game { get; set; }    
        public string Genre { get; set; }   

        public string[] Plateforms { get; set; }
        public int TotalPlayTime { get; set; }

        public int TotalPlayers { get; set; }
    }
}
