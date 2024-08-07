using SistemaDeCadastroDeCliente.Data;
using SistemaDeCadastroDeCliente.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeCadastroDeCliente.Repositorio
{
    public class FornecedorRepositorio : IFornecedorRepositorio
    {
        private readonly BancoContext _bancoContext;

        public FornecedorRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public FornecedorModel ListarPorId(int id)
        {
            return _bancoContext.Fornecedores.FirstOrDefault(x => x.ID == id);
        }

        public List<FornecedorModel> BuscarTodos()
        {
            return _bancoContext.Fornecedores.ToList();
        }

        public FornecedorModel Adicionar(FornecedorModel fornecedor)
        {
            _bancoContext.Fornecedores.Add(fornecedor);
            _bancoContext.SaveChanges();

            return fornecedor;
        }

        public FornecedorModel Atualizar(FornecedorModel fornecedor)
        {
            FornecedorModel fornecedorDB = ListarPorId(fornecedor.ID);

            if (fornecedorDB == null) throw new System.ArgumentNullException("Falha ao atualizar");

            fornecedorDB.Nome = fornecedor.Nome;
            fornecedorDB.CNPJ = fornecedor.CNPJ;
            fornecedorDB.Segmento = fornecedor.Segmento;
            fornecedorDB.CEP = fornecedor.CEP;
            fornecedorDB.Endereco = fornecedor.Endereco;

            _bancoContext.Fornecedores.Update(fornecedorDB);
            _bancoContext.SaveChanges();

            return fornecedorDB;
        }

        public bool Apagar(int id)
        {
            FornecedorModel fornecedorDB = ListarPorId(id);

            if (fornecedorDB == null) throw new System.ArgumentNullException("Falha ao apagar");

            _bancoContext.Fornecedores.Remove(fornecedorDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
