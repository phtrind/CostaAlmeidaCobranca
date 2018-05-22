using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class EventoEntidade : EntidadeBase
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }

        public string Nome { get; set; }
        public DateTime Data { get; set; }

        public long? IdUsuario { get; set; }
        public long? IdEndereco { get; set; }

        public UsuarioEntidade Usuario { get; set; }
        public EnderecoEntidade Endereco { get; set; }
        public List<ContratoEntidade> Contratos { get; set; }
    }
}
