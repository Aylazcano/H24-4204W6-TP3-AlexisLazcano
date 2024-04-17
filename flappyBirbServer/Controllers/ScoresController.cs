using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using flappyBirb_server.Data;
using flappyBirb_server.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using flappyBirbServer.Models;

namespace flappyBirbServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly FlappyBirbContext _context;

        public ScoresController(FlappyBirbContext context)
        {
            _context = context;
        }

        // GET: api/Scores/GetPublicScores
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Score>>> GetPublicScores()
        {
            var publicScores = await _context.Score.Where(s => s.IsPublic).ToListAsync();
            if (publicScores == null)
            {
                return NotFound("No public scores found.");
            }
            return publicScores;
        }

        // GET: api/Scores/GetMyscores
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<Score>>> GetMyScores()
        {
            if (_context.Score == null || !_context.Score.Any())
            {
                return NotFound("No scores found.");
            }

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            BirbUser? user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                return user.Scores;
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new { Message = "User not found." });
        }

        // PUT: api/Scores/ChangeScoreVisibility/{id}
        [Authorize]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeScoreVisibility(int id, Score score)
        {
            if (id != score.Id)
            {
                return BadRequest("The score ID in the request body does not match the one in the URL.");
            }

            var existingScore = await _context.Score.FindAsync(id);
            if (existingScore == null)
            {
                return NotFound("Score not found.");
            }

            existingScore.IsPublic = !existingScore.IsPublic;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
                {
                    return NotFound("Score not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Scores/PostScore
        [Authorize]
        [HttpPost("[action]")]
        public async Task<ActionResult<Score>> PostScore(ScoreDTO scoreDTO)
        {
            if (_context.Score == null)
            {
                return Problem("Entity set 'FlappyBirbContext.Score' is null.");
            }

            // Trouve l'utilisateur qui a envoyé la requête grace à son Token
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BirbUser? user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                Score score = new Score
                {
                    Date = DateTime.Now,
                    TimeInSeconds = scoreDTO.TimeInSeconds,
                    ScoreValue = scoreDTO.ScoreValue,
                    IsPublic = scoreDTO.IsPublic,
                    BirbUser = user
                };
     
                // On ajoute le score à l'utilisateur
                user.Scores.Add(score);

                // On ajoute le score à la BD
                _context.Score.Add(score);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetScore", new { id = score.Id }, score);
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new { Message = "User not found." });
        }

        private bool ScoreExists(int id)
        {
            return (_context.Score?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
