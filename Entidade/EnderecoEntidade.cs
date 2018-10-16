using System;

namespace Entidade
{
    public class EnderecoEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }
    }
}
