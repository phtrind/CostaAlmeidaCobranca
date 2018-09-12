namespace Projecao
{
    public class InformacoesUsuarioResponse
    {
        public long IdUsuario { get; set; }
        public long IdInterno { get; set; }
        public int TipoUsuario { get; set; }
        public int? Permissao { get; set; }
        public string Nome { get; set; }
    }
}
