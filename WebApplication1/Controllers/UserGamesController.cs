using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Models;
using WebApplication1.ModelView;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserGamesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGame>>> GetUserGame()
        {
            return await _context.UserGame.Include(p => p.User).Include(p => p.Game).ToListAsync();
        }

        // GET: api/UserGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGame>> GetUserGame(int id)
        {
            var userGame = await _context.UserGame.Include(p => p.User).Include(p => p.Game).FirstOrDefaultAsync(m => m.Id == id);

            if (userGame == null)
            {
                return NotFound();
            }

            return userGame;
        }
        


        // POST: api/UserGames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Response> PostUserGame(UserGameQuerry userGame)
        {
            Response response = new Response();
            try { 
            var exist =  _context.UserGame.Any(us=> us.UserId == userGame.UserId && us.GameId == userGame.GameId);
            if(exist)
            {
                var userg = await _context.UserGame.Where(us => us.UserId == userGame.UserId && us.GameId == userGame.GameId).FirstOrDefaultAsync();
                userg.TotalPlayTime += userGame.TotalPlayTime;

                _context.UserGame.Update(userg);


                await _context.SaveChangesAsync();
                response.Text = "This user already a player total time added ...";
                return response;
            }
            var games = await _context.Game.FindAsync(userGame.GameId);
            if (games == null)
            {
               
                response.Text = "Game not found";
                return response;
            }
            var user = await _context.User.FindAsync(userGame.UserId);
            if (user == null)
            {
              
                response.Text = "user not found";
                return response;
               
            }
            UserGame UG = new UserGame
            {
               
                UserId = userGame.UserId,
                GameId = userGame.GameId,
                TotalPlayTime = userGame.TotalPlayTime,
            };
            _context.UserGame.Add(UG);
            await _context.SaveChangesAsync();
            
            response.Text = "Success creation";
            return response;
            }
            catch
            {
                response.Text = "bad request";
                return response;
            }


        }

        // DELETE: api/UserGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGame(int id)
        {
            var userGame = await _context.UserGame.FindAsync(id);
            if (userGame == null)
            {
                return NotFound();
            }

            _context.UserGame.Remove(userGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserGameExists(int id)
        {
            return _context.UserGame.Any(e => e.Id == id);
        }
    }
}
