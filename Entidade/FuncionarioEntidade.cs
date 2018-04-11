using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class FuncionarioEntidade : EntidadeBase
    {
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public UsuarioEntidade Usuario { get; set; }
    }
}
