using Microsoft.AspNetCore.Authorization;
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
    [Route("api/gamePlayers")]
    [ApiController]
    [Authorize("PlayerOnly")]
    public class GamePlayersController : ControllerBase
    {
        private IGamePlayerRepository _repository;
        private IPlayerRepository _playerRepository;

        public GamePlayersController(IGamePlayerRepository repository,IPlayerRepository playerRepository)
        {
            _repository = repository;
            _playerRepository = playerRepository;
        }

        // GET api/<GamePlayersController>/5
        [HttpGet("{id}", Name = "GetGameView")]
        public IActionResult GetGameView(long id)
        {
            try
            {
                string email = User.FindFirst("Player") != null ? User.FindFirst("Player").Value : "Guest";
                //Obtención del Gameplayer
                var gp = _repository.GetGamePlayerView(id);
                // Verificar si el Gameplayer corresponde al mismo email del usuario autenticado
                if(gp.Player.Email != email)
                {
                    return Forbid();
                }
                var gameView = new GameViewDTO
                {
                    Id = gp.Id,
                    CreationDate = gp.Game.CreationDate,
                    Ships = gp.Ships.Select(ship => new ShipDTO
                    {
                        Id = ship.Id,
                        Type = ship.Type,
                        Locations = ship.Locations.Select(shipLocation => new ShipLocationDTO
                        {
                            Id = shipLocation.Id,
                            Location = shipLocation.Location
                        }).ToList()
                    }).ToList(),
                    GamePlayers = gp.Game.GamePlayers.Select(gps => new GamePlayerDTO
                    {
                        Id = gps.Id,
                        JoinDate = gps.JoinDate,
                        Player = new PlayerDTO
                        {
                            Id = gps.Player.Id,
                            Email = gps.Player.Email
                        }
                    }).ToList(),
                    Salvos = gp.Game.GamePlayers.SelectMany(gps => gps.Salvos.Select(salvo => new SalvoDTO
                    {
                        Id = salvo.Id,
                        Turn = salvo.Turn,
                        Player = new PlayerDTO
                        {
                            Id = gps.Player.Id,
                            Email = gps.Player.Email
                        },
                        Locations = salvo.Locations.Select(salvoLocation => new SalvoLocationDTO
                        {
                            Id = salvoLocation.Id,
                            Location = salvoLocation.Location
                        }).ToList()
                    })).ToList()

                };


                return Ok(gameView);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id}/ships")]
        public IActionResult PostShips (long id, [FromBody] List<ShipDTO> ships)
        {
            try
            {
                // Vamos a buscar al jugador autenticado
                string email = User.FindFirst("Player") != null ? User.FindFirst("Player").Value : "Guest";
                Player player = _playerRepository.FindByEmail(email);


                GamePlayer gamePlayer = _repository.FindById(id);

                // Verificar que el juego con el id indicado exista
                if (gamePlayer == null)
                    return StatusCode(403, "No existe el Juego");

                // Verificar si el usuario se encuentra en el juego donde se quieren agregar barcos
                if (gamePlayer.Player.Id != player.Id)
                    return StatusCode(403, "El usuario no se encuentra en el juego");

                // Verificar si ya se han posicionado los barcos
                if (gamePlayer.Ships.Count() == 5)
                    return StatusCode(403, "Ya se han posicionado los barcos");

                //Insertar los barcos en el GamePlayer
                gamePlayer.Ships = ships.Select(ship => new Ship
                {
                    Type = ship.Type,
                    GamePlayerId = gamePlayer.Id,
                    Locations = ship.Locations.Select(location => new ShipLocation
                    {
                        Location = location.Location,
                        ShipId = ship.Id
                    }).ToList()
                }).ToList();

                // Guardar en la bd
                _repository.Save(gamePlayer);
                return StatusCode(201, gamePlayer.Id);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id}/salvos")]
        public IActionResult PostSalvos(long id, [FromBody] SalvoDTO salvo)
        {
            try
            {
                // Vamos a buscar al jugador autenticado
                string email = User.FindFirst("Player") != null ? User.FindFirst("Player").Value : "Guest";
                Player player = _playerRepository.FindByEmail(email);

                GamePlayer gamePlayer = _repository.FindById(id);

                // Verificar que el juego con el id indicado exista
                if (gamePlayer == null)
                    return StatusCode(403, "No existe el Juego");

                // Verificar si el usuario se encuentra en el juego donde se quieren agregar barcos
                if (gamePlayer.Player.Id != player.Id)
                    return StatusCode(403, "El usuario no se encuentra en el juego");

                // Obtener el Gameplayer Oponente
                GamePlayer opponentGamePlayer = gamePlayer.getOpponent();

                // Verificar que el oponente existe
                if (opponentGamePlayer == null)
                    return StatusCode(403, "No hay oponente");

                // Verificar si el oponente posicionó los Ships
                if (gamePlayer.Ships.Count() == 0)
                    return StatusCode(403, "Debe posicionar los barcos");
                if (opponentGamePlayer.Ships.Count() == 0)
                    return StatusCode(403, "El oponente no ha posicionado sus barcos");

                    // Verificar que se han posicionado los barcos
                    //if (gamePlayer.Ships.Count() != 5 || opponentGamePlayer.Ships.Count() != 5)
                    //    return StatusCode(403, "Aun no se han posicionado los barcos");

                int playerTurn = 0;
                int opponentTurn = 0;

                playerTurn = gamePlayer.Salvos != null ? gamePlayer.Salvos.Count() + 1 : 1;

                if (opponentGamePlayer != null)
                    opponentTurn = opponentGamePlayer.Salvos != null ? opponentGamePlayer.Salvos.Count() : 0;

                if (playerTurn - opponentTurn > 1)
                    return StatusCode(403, "No se puede adelantar");

                // Agregar el Salvo
                gamePlayer.Salvos.Add(new Models.Salvo
                {
                    Turn = playerTurn,
                    GamePlayerId = gamePlayer.Id,
                    Locations = salvo.Locations.Select(location => new SalvoLocation
                    {
                        Location = location.Location,
                        SalvoId = salvo.Id
                    }).ToList()
                });

                // Guardar en la bd
                _repository.Save(gamePlayer);
                return StatusCode(201, gamePlayer.Id);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
