using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class UsuarioEntidade : EntidadeBase
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuarioEnum Tipo { get; set; }
    }
}