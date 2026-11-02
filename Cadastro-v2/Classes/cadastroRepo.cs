using Cadastro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadastro
{
    public class cadastroRepo : IRepositorio<CadastroProj>
    {
        private readonly Dictionary<int, CadastroProj> cadastros = new Dictionary<int, CadastroProj>();
        private readonly object gate = new object();
        private int nextId;
        public void Atualiza(int id, CadastroProj entidade)
        {
            if (entidade == null || entidade.Id != id) throw new ArgumentException("O ID da entidade deve corresponder ao solicitado.");
            lock (gate) {
                if (!cadastros.TryGetValue(id, out var atual) || atual.retornaExcluido()) throw new KeyNotFoundException("Funcionário não encontrado.");
                cadastros[id] = entidade;
            }
        }
        public void Exclui(int id)
        {
            lock (gate) {
                if (!cadastros.TryGetValue(id, out var entidade) || entidade.retornaExcluido()) throw new KeyNotFoundException("Funcionário não encontrado.");
                entidade.Excluir();
            }
        }
        public void Insere(CadastroProj entidade)
        {
            if (entidade == null) throw new ArgumentNullException(nameof(entidade));
            lock (gate) {
                if (cadastros.ContainsKey(entidade.Id)) throw new ArgumentException("ID já cadastrado.");
                int seguinte = checked(entidade.Id + 1);
                cadastros.Add(entidade.Id, entidade);
                nextId = Math.Max(nextId, seguinte);
            }
        }
        public List<CadastroProj> Lista() { lock (gate) { return cadastros.Values.OrderBy(c => c.Id).ToList(); } }
        public int ProximoId() { lock (gate) { int id = nextId; nextId = checked(nextId + 1); return id; } }
        public CadastroProj? RetornarPorId(int id) {
            lock (gate) { return cadastros.TryGetValue(id, out var entidade) && !entidade.retornaExcluido() ? entidade : null; }
        }
    }
}
