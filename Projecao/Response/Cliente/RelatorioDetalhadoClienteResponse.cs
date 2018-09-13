namespace Projecao
{
    public class RelatorioDetalhadoClienteResponse
    {
        #region .: Cliente :.

        public long IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string TipoDocumento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Fazenda { get; set; }

        #endregion

        #region .: Endereço :.

        public long IdEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }

        #endregion
    }
}
