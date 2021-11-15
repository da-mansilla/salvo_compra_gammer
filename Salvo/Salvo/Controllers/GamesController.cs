using Microsoft.AspNetCore.Mvc;
using Salvo.Models;
using Salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Salvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGameRepository _repository;
        public GamesController(IGameRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<GamesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Necesitamos una lista de games
                // que finalmente es una lista de gamesDTO
                // var games = _repository.GetAllGamesWithPlayers();
                var games = _repository.GetAllGamesWithPlayers()
                    .Select(g => new GameDTO
                    {
                        Id = g.Id,
                        CreationDate = g.CreationDate,
                        GamePlayers = g.GamePlayers.Select(gp => new GamePlayerDTO
                        {
                            Id = gp.Id,
                            JoinDate = gp.JoinDate,
                            Player = new PlayerDTO
                            {
                                Id = gp.Player.Id,
                                Email = gp.Player.Email
                            },
                            Point = gp.GetScore() != null ? (double?)gp.GetScore().Point : null
                        }).ToList()
                    }).ToList();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        
    }
}
