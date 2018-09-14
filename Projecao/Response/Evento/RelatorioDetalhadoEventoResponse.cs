using System;

namespace Projecao
{
    public class RelatorioDetalhadoEventoResponse
    {
        public long IdEvento { get; set; }
        public string Nome { get; set; }
        public string Data { get; set; }
        public DateTime DataDateTime { get; set; }

        public long IdEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
