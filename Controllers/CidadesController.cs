using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Projetos_API.Models;
using Projetos_API.Repository;
using System.ComponentModel.DataAnnotations;

namespace Projetos_API.Controllers
{
    [ApiController]
    [Route("paises/{idPais}/estados/{idEstado}/cidades")]
    public class CidadesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Cidade>> GetCidades(
            [FromQuery] string? nome, 
            [FromQuery, Range(100, 100000)] int fromPopulacao,
            [FromRoute, Required] int idPais,
            [FromRoute, Required] int idEstado
            )
        {
            var resultado = CidadesRepository.Cidades.Where(cidade => cidade.IdPais == idPais &&
            cidade.IdEstado == idEstado).ToList();

            if(resultado.Count == 0) return NotFound(null);

            if (!string.IsNullOrEmpty(nome))
            {
                resultado = resultado.Where(cidade => cidade.Nome == nome).ToList();
            }

            if (fromPopulacao > 0)
            {
                resultado = resultado.Where(cidade => cidade.Populacao >= fromPopulacao).ToList();
            }
            return Ok(resultado);
        }

        [HttpPost]
        public ActionResult PostCidades(
            [FromRoute, Required] int idPais,
            [FromRoute, Required] int idEstado,
            [FromBody] Cidade cidade
            )
        {
            cidade.IdEstado = idEstado;
            cidade.IdPais = idPais;
            CidadesRepository.Cidades.Add(cidade);
            CidadesRepository.Save();

            string locationUrl = $"paises/{idPais}/estados/{idEstado}/cidades/{cidade.Id}";

            return Created(locationUrl, null);
        }  
    }
}