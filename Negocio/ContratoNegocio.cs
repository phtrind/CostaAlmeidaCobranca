using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projecao;
using Enumerador;

namespace Negocio
{
    public class ContratoNegocio : NegocioBase<ContratoEntidade>
    {
        public IEnumerable<ContratoEntidade> ListarTodosCompleto()
        {
            return new ContratoDados().ListarTodosCompleto();
        }

        public ContratoEntidade ListarCompleto(long aCodigo)
        {
            return new ContratoDados().ListarCompleto(aCodigo);
        }

        public GetCombosCadastroContratoResponse getCombosCadastroContrato()
        {
            var comboClientes = new ClienteNegocio().getComboClientes().ToList();

            return new GetCombosCadastroContratoResponse()
            {
                Clientes = comboClientes,
                Eventos = comboClientes
            };
        }

        public string TraduzirStatus(StatusContratoEnum status)
        {
            switch (status)
            {
                case StatusContratoEnum.Ativo:
                    return "Ativo";
                case StatusContratoEnum.Suspenso:
                    return "Suspenso";
                case StatusContratoEnum.Cancelado:
                    return "Cancelado";
                default:
                    return string.Empty;
            }
        }

        public bool ValidarCadastroContrato(ContratoEntidade aEntidade)
        {
            if (aEntidade.Valor == 0 || 
                aEntidade.Parcelas == null || 
                !aEntidade.Parcelas.Any() || 
                aEntidade.Parcelas.Any(x => x.Valor == 0))
            {
                return false;
            }

            if (aEntidade.Parcelas.Sum(x => x.Valor) != aEntidade.Valor)
                return false;

            if (string.IsNullOrEmpty(aEntidade.Animal))
                return false;

            if (!aEntidade.IdVendedor.HasValue || !aEntidade.IdComprador.HasValue || !aEntidade.IdUsuario.HasValue)
                return false;

            return true;
        }
    }
}
