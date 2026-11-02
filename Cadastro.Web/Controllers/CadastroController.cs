using Cadastro.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadastro.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly IRepositorio<CadastroProj> repo;
        public CadastroController(IRepositorio<CadastroProj> repositorioCadastro) { repo = repositorioCadastro; }
        [HttpGet("")]
        public IActionResult Lista() => Ok(repo.Lista().Select(c => new CadastroModel(c)).ToList());
        [HttpGet("{id:int}")]
        public IActionResult Consulta(int id) {
            var cadastro = repo.RetornarPorId(id);
            return cadastro == null ? NotFound() : Ok(new CadastroModel(cadastro));
        }
        [HttpPost("")]
        public IActionResult Insere([FromBody] CadastroModel model) {
            try {
                model.Id = repo.ProximoId();
                var cadastro = model.ToCadastroProj(); repo.Insere(cadastro);
                return CreatedAtAction(nameof(Consulta), new {id = cadastro.Id}, new CadastroModel(cadastro));
            } catch (ArgumentException error) { return BadRequest(new {message = error.Message}); }
        }
        [HttpPut("{id:int}")]
        public IActionResult Atualiza(int id, [FromBody] CadastroModel model) {
            try {
                if (repo.RetornarPorId(id) == null) return NotFound();
                model.Id = id; repo.Atualiza(id, model.ToCadastroProj()); return NoContent();
            } catch (KeyNotFoundException) { return NotFound(); }
              catch (ArgumentException error) { return BadRequest(new {message = error.Message}); }
        }
        [HttpDelete("{id:int}")]
        public IActionResult Exclui(int id) {
            try { repo.Exclui(id); return NoContent(); }
            catch (KeyNotFoundException) { return NotFound(); }
        }
    }
}
