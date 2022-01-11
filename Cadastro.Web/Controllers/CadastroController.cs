using Cadastro.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro.Web.Controllers
{
    [Route("[controller]")]
    public class CadastroController : Controller
    {
        private readonly IRepositorio<CadastroProj> _repositorioCadastro;

        public CadastroController(IRepositorio<CadastroProj> repositorioCadastro)
        {
            _repositorioCadastro = repositorioCadastro;
        }

        [HttpGet("")]
        public IActionResult Lista()
        {
            return Ok(_repositorioCadastro.Lista().Select(c => new CadastroModel(c)));

            IList<CadastroModel> cadastro = new List<CadastroModel>();

        }

        [HttpPut("{id}")]
        public IActionResult Atualiza(int id, [FromBody] CadastroModel model)
        {
            _repositorioCadastro.Atualiza(id, model.ToCadastroProj());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Exclui(int id)
        {
            _repositorioCadastro.Exclui(id);
            return NoContent();
        }

        [HttpPost("")]
        public IActionResult Insere([FromBody] CadastroModel model)
        {
            model.Id = _repositorioCadastro.ProximoId();

            CadastroProj cadastroProj = model.ToCadastroProj();

            _repositorioCadastro.Insere(cadastroProj);
            return Created("", cadastroProj);
        }

        [HttpGet("{id}")]
        public IActionResult Consulta(int id)
        {
            return Ok(new CadastroModel(_repositorioCadastro.Lista().FirstOrDefault(c => c.Id == id)));
        }
    }
}
