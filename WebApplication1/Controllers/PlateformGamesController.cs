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
    public class PlateformGamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlateformGamesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PlateformGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlateformGame>>> GetPlateformGame()
        {
            return await _context.PlateformGame.Include(p => p.Platform).Include(p => p.Game).ToListAsync();
        }

        // GET: api/PlateformGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlateformGame>> GetPlateformGame(int id)
        {
            var plateformGame = await _context.PlateformGame.FindAsync(id);

            if (plateformGame == null)
            {
                return NotFound();
            }

            return plateformGame;
        }

        

        // POST: api/PlateformGames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Response> PostPlateformGame(PlateformeGameQuerry plateformGame)
        {
            Response response = new Response();
            try {
           
            var exist = _context.PlateformGame.Any(pl => pl.GameId == plateformGame.GameId && pl.PlatformId == plateformGame.PlatformId);
            if (exist)
            {
                response.Text = "the game is already under this plateforme";
                return response;
            }
                var games = await _context.Game.FindAsync(plateformGame.GameId);
                if (games == null)
                {

                    response.Text = "Game not found";
                    return response;
                }
                var palteforme = await _context.Platform.FindAsync(plateformGame.PlatformId);
                if (palteforme == null)
                {

                    response.Text = "plateform not found";
                    return response;
                }

                PlateformGame plg = new PlateformGame
            {
                GameId = plateformGame.GameId,
                PlatformId = plateformGame.PlatformId,
            };

            _context.PlateformGame.Add(plg);
            await _context.SaveChangesAsync();
            response.Text = "Success";
            return response;
            }
            catch
            {
                response.Text = "bad request";
                return response;
            }

        }

        // DELETE: api/PlateformGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlateformGame(int id)
        {
            var plateformGame = await _context.PlateformGame.FindAsync(id);
            if (plateformGame == null)
            {
                return NotFound();
            }

            _context.PlateformGame.Remove(plateformGame);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlateformGameExists(int id)
        {
            return _context.PlateformGame.Any(e => e.Id == id);
        }
    }
}
