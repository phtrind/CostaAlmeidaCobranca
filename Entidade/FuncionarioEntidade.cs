using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class FuncionarioEntidade : EntidadeBase
    {
        public long? Id { get; set; }
        public DateTime? DataCadastro { get; set; }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TelefoneFixo { get; set; }
        public string TelefoneCelular { get; set; }
        public PermissaoFuncionarioEnum Permissao { get; set; }

        public long? IdUsuario { get; set; }
        public UsuarioEntidade Usuario { get; set; }
    }
}
