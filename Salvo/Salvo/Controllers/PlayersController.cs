using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Salvo.Models;
using Salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IPlayerRepository _repository;
        public PlayersController(IPlayerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PlayerDTO player)
        {
            try
            {
                // Verificar email y password no vacios
                 // TODO agregar una validacion de seguridad respecto de la clave
                 // cantidad de caracteres, mayusculas, numeros
                 // verificar estandar de claves
                if (String.IsNullOrEmpty(player.Email))
                    return StatusCode(403, "Email Vacio");
                if (String.IsNullOrEmpty(player.Email))
                    return StatusCode(403, "Password Vacia");

                Player dbPlayer = _repository.FindByEmail(player.Email);
                if (dbPlayer != null)
                    return StatusCode(403, "Email está en uso");

                Player newPlayer = new Player
                {
                    Email = player.Email,
                    Password = player.Password,
                    Name = player.Name
                };
                _repository.Save(newPlayer);
                // Retornamos el nuevo jugador
                return StatusCode(201, newPlayer);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
