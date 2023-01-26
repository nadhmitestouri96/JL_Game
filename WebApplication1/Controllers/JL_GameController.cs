using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ModelView;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JL_GameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JL_GameController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<List<JL_GameResponse>> GetData()
        {
            List<JL_GameResponse> response = new List<JL_GameResponse>();

            var US = await _context.UserGame.Include(p => p.User).Include(p=> p.Game).ToListAsync();
            foreach (var item in US)
            {
                var userid = item.UserId;
               
                var gameName = item.Game.Name;
                var  genre = item.Game.Genre;
                var playtime = item.TotalPlayTime;
                List<string> plateforms = new List<string>();
                var pls = await _context.PlateformGame.Include(p => p.Platform).Include(p=> p.Game).Where(pg => pg.GameId == item.GameId).ToListAsync();  
                
                foreach(var form in pls)
                {
                    
                  
                    var plateformName = form.Platform.Name;
                    plateforms.Add(plateformName);

                }
                string[] plname = plateforms.ToArray();
                JL_GameResponse jl = new JL_GameResponse
                {
                    UserId = userid,
                    Game = gameName,
                    PlayTime = playtime,
                    Genre = genre,
                    Platforms = plname,
                };
                response.Add(jl);   

            }
            return response;
        }


        [HttpGet("select_top_by_playtime")]
        public async Task<GameResponse> select_top_by_playtime(string? genre, string? platforms)
        {
            GameResponse topGame = new GameResponse();
            int MaxplayedGameTime = 0;
            var games = await GetGamebyplateformeandgenre(genre, platforms);


            foreach (var game in games)
            {
                int playtime = 0;
                var gameid = game.Id; 
                var UG = await _context.UserGame.Where(UG => UG.GameId == gameid).ToListAsync();
                
                playtime = UG.Sum(UG => UG.TotalPlayTime);
               
               if(playtime > MaxplayedGameTime)
                {
                    MaxplayedGameTime = playtime;
                    var pls = await _context.PlateformGame.Include(p=> p.Game).Include(p=> p.Platform).Where(pg => pg.GameId == game.Id).ToListAsync();
                    var userGame = await _context.UserGame.Where(pg => pg.GameId == game.Id).ToListAsync();
                    var totalplayers = userGame.Count();
                    List<string> plateforms = new List<string>();
                    foreach (var form in pls)
                    {
                       
                        var plateformName = form.Platform.Name;
                        plateforms.Add(plateformName);

                    }
                    string[] plname = plateforms.ToArray();
                    var tg = await _context.Game.FindAsync(gameid);
                    topGame.Game = tg.Name;
                    topGame.Genre = tg.Genre;
                    topGame.TotalPlayTime = playtime;
                    topGame.Plateforms = plname;
                    topGame.TotalPlayers = totalplayers;    
                }
             

            }
            return topGame;

        }


        [HttpGet("select_top_by_players")]
        public async Task<GameResponse> select_top_by_players(string? genre, string? platforms)
        {
            GameResponse topGame = new GameResponse();
            int MaxplayedGame = 0;
           var games = await  GetGamebyplateformeandgenre(genre, platforms);
           

            foreach (var game in games)
            {
                int player = 0;
                var gameid = game.Id;
                var UG = await _context.UserGame.Where(UG => UG.GameId == gameid).ToListAsync();
                var playtime = UG.Sum(UG => UG.TotalPlayTime);
                player = UG.Count();

                if (player > MaxplayedGame)
                {
                    MaxplayedGame = player;
                    var pls = await _context.PlateformGame.Include(p => p.Game).Include(p => p.Platform).Where(pg => pg.GameId == game.Id).ToListAsync();
                    var userGame = await _context.UserGame.Where(pg => pg.GameId == game.Id).ToListAsync();
                    var totalplayers = userGame.Count();
                    List<string> plateforms = new List<string>();
                    foreach (var form in pls)
                    {

                        var plateformName = form.Platform.Name;
                        plateforms.Add(plateformName);

                    }
                    string[] plname = plateforms.ToArray();
                    var tg = await _context.Game.FindAsync(gameid);
                    topGame.Game = tg.Name;
                    topGame.Genre = tg.Genre;
                    topGame.TotalPlayTime = playtime;
                    topGame.Plateforms = plname;
                    topGame.TotalPlayers = totalplayers;
                }


            }
            return topGame;

        }
        private async Task<List<Game>> GetGamebyplateformeandgenre(string? genre, string? platforms)
        {
           
           
            var games = await _context.Game.ToListAsync();

            if (genre != null)
            {
                if (platforms != null)
                {
                    var plateformGame = await _context.PlateformGame.Include(p => p.Platform).Include(p => p.Game).Where(p => p.Platform.Name == platforms).Where(p => p.Game.Genre == genre).ToListAsync();
                    List<Game> g = new List<Game>();
                    foreach (var game in plateformGame)
                    {
                        g.Add(game.Game);
                    }
                    games = g;
                }

                else
                {
                    games = await _context.Game.Where(g => g.Genre == genre).ToListAsync();
                }

            }
            if (platforms != null && genre == null)
            {
                var plateformGame = await _context.PlateformGame.Include(p => p.Platform).Include(p => p.Game).Where(p => p.Platform.Name == platforms).ToListAsync();
                List<Game> g = new List<Game>();
                foreach (var game in plateformGame)
                {
                    g.Add(game.Game);
                }
                games = g;
            }
            return games;

        }
       
    }
}
