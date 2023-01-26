namespace WebApplication1.ModelView
{
    public class JL_GameResponse
    {
        public int UserId { get; set; } 
        public string Game { get; set; }
        public int PlayTime { get; set; }
        public string Genre { get; set; }

        public string[] Platforms { get; set; } 

    }
}
