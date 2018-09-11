using Dados;
using Entidade;
using Enumerador;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if (!aEntidade.IdVendedor.HasValue || !aEntidade.IdComprador.HasValue || !aEntidade.IdUsuarioCadastro.HasValue)
            {
                return false;
            }

            return true;
        }

        public override void ValidateRegister(ContratoEntidade aEntidade)
        {
            if (aEntidade.Valor == default(decimal))
            {
                throw new Exception("O valor do contrato é inválido.");
            }

            if (aEntidade.Parcelas == null)
            {
                throw new Exception("Não é possível cadastrar um contrato sem parcelas.");
            }

            if (aEntidade.Valor != aEntidade.Parcelas.Sum(x => x.Valor))
            {
                throw new Exception("O valor do contrato não pode ser diferente da soma do valor das parcelas");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue)
            {
                throw new Exception("É obrigatório informar o usuário resposável pelo cadastro.");
            }

            var negocioParcelas = new ParcelaNegocio();

            foreach (var parcela in aEntidade.Parcelas)
            {
                negocioParcelas.ValidateRegister(parcela);
            }
        }
    }
}
