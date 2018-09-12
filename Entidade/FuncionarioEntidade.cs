using Enumerador;
using System;

namespace Entidade
{
    public class FuncionarioEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public PermissaoFuncionarioEnum Permissao { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdUsuario { get; set; }
        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public UsuarioEntidade Usuario { get; set; }
        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }
    }
}