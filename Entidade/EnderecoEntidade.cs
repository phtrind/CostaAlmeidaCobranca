using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class EnderecoEntidade : EntidadeBase
    {
        public long? Id { get; set; }
        public DateTime? DataCadastro { get; set; }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public long Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
