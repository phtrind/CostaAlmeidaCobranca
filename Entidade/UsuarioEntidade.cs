using Enumerador;
using System;

namespace Entidade
{
    public class UsuarioEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuarioEnum Tipo { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }
    }
}