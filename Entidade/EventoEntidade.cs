using System;
using System.Collections.Generic;

namespace Entidade
{
    public class EventoEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public string Nome { get; set; }
        public DateTime Data { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdEndereco { get; set; }
        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public EnderecoEntidade Endereco { get; set; }
        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }

        public List<ContratoEntidade> Contratos { get; set; }
    }
}
