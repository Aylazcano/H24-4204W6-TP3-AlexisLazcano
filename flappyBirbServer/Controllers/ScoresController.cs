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
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
        public async Task<ActionResult<IEnumerable<ScoreDTO>>> GetPublicScores()
        {
            IEnumerable<Score> publicScores = await _context.Score.Where(s => s.IsPublic == true).ToListAsync();
            if (publicScores == null)
            {
                return NotFound("No public scores found.");
            }
            return Ok(publicScores.Select(s => new ScoreDTO
            {
                Id = s.Id,
                Pseudo = s.BirbUser?.UserName,
                Date = s.Date,
                TimeInSeconds = s.TimeInSeconds,
                ScoreValue = s.ScoreValue,
                IsPublic = s.IsPublic
            }));

        }

        // GET: api/Scores/GetMyscores
        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<ScoreDTO>>> GetMyScores()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            BirbUser? user = await _context.Users.FindAsync(userId);
            IEnumerable<Score> myScores = await _context.Score.Where(s => s.BirbUser == user).ToListAsync();
            if (myScores == null)
            {
                return NotFound("No user scores found.");
            }
            if (user != null)
            {
                return Ok(myScores.Select(s => new ScoreDTO
                {
                    Id = s.Id,
                    Pseudo = s.BirbUser?.UserName,
                    Date = s.Date,
                    TimeInSeconds = s.TimeInSeconds,
                    ScoreValue = s.ScoreValue,
                    IsPublic = s.IsPublic
                }));
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new { Message = "User not found." });
        }

        // PUT: api/Scores/ChangeScoreVisibility/{id}
        [Authorize]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ChangeScoreVisibility(ScoreDTO scoreDTO)
        {
            // Trouve l'utilisateur qui a envoyé la requête grace à son Token
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BirbUser? user = await _context.Users.FindAsync(userId);

            Score existingScore = await _context.Score.FindAsync(scoreDTO.Id);

            if (existingScore == null)
            {
                return NotFound("Score not found.");
            }

            if (user == null || !user.Scores.Any(s => s.Id == existingScore.Id))
            {
                return Unauthorized(new { Message = "You are not allowed to change the visibility of this score." });
            }

            existingScore.IsPublic = !existingScore.IsPublic;
            _context.Entry(existingScore).State = EntityState.Modified;

            //try
            //{
                await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (await _context.Score.FindAsync(scoreDTO.Id) == null)
            //        return NotFound();
            //    else
            //        throw;
            //}


            return Ok(existingScore);
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
                    Id = 0,
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
                return Ok(score);
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
