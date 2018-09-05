using System;
using System.Collections.Generic;

namespace Entidade
{
    public class ClienteEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public string Fazenda { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdEndereco { get; set; }
        public long? IdUsuario { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public EnderecoEntidade Endereco { get; set; }
        public List<ContratoEntidade> Contratos { get; set; }
        public List<ContasBancariasEntidade> ContasBancarias { get; set; }
        public UsuarioEntidade Usuario { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }
    }
}
