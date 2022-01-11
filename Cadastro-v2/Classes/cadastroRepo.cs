using Cadastro.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Cadastro
{
    public class cadastroRepo : IRepositorio<CadastroProj>
    {
        private List<CadastroProj> listCadastroProj = new List<CadastroProj>();
        public void Atualiza(int id, CadastroProj entidade)
        {
            listCadastroProj[id] = entidade;        
        }

        public void Exclui(int id)
        {
            listCadastroProj[id].Excluir();
        }

        public void Insere(CadastroProj entidade)
        {
            listCadastroProj.Add(entidade);
        }

        public List<CadastroProj> Lista()
        {
            return listCadastroProj;
        }

        public int ProximoId()
        {
            return listCadastroProj.Count;
        }

        public CadastroProj RetornarPorId(int id)
        {
            return listCadastroProj.FirstOrDefault(_ => _.Id == id);
        }
    }
}