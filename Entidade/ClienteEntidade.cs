using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class ClienteEntidade : EntidadeBase
    {
        public long? Id { get; set; }
        public DateTime? DataCadastro { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public DateTime? DataNascimento { get; set; } //remover

        public long? IdEndereco { get; set; }
        public long? IdUsuario { get; set; }

        public EnderecoEntidade Endereco { get; set; }
        public List<ContratoEntidade> Contratos { get; set; }
        public List<ContasBancariasEntidade> ContasBancarias { get; set; }
        public UsuarioEntidade Usuario { get; set; }
    }
}
