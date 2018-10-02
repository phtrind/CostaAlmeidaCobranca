using Dados;
using Entidade;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio
{
    public class CrmNegocio : NegocioBase<CrmEntidade>
    {
        #region .: Relatório :.

        public IEnumerable<CrmProjecao> BuscarPorContrato(long aId)
        {
            return new CrmDados().BuscarPorContrato(aId).Select(x => new CrmProjecao()
            {
                Id = Convert.ToString(x.CRM_CODIGO),
                Descricao = Convert.ToString(x.CRM_DESCRICAO),
                UsuarioCadastro = Convert.ToString(x.FUN_NOME),
                DataHoraCadastro = Convert.ToDateTime(x.CRM_DTHCADASTRO).ToString("dd/MM/yyyy HH:mm")
            });
        }

        #endregion

        #region .: Cadastro :.

        public long Cadastrar(CrmEntidade aEntidade)
        {
            aEntidade.DataCadastro = DateTime.Now;

            return Inserir(aEntidade);
        }

        public override void ValidateRegister(CrmEntidade aEntidade, bool isEdicao)
        {
            if (string.IsNullOrEmpty(aEntidade.Descricao))
            {
                throw new Exception("A descrição não foi informada.");
            }

            if (!aEntidade.IdContrato.HasValue)
            {
                throw new Exception("O contrato não foi informado.");
            }

            if (new ContratoNegocio().Listar(aEntidade.IdContrato.Value) == null)
            {
                throw new Exception("O contrato informado não foi encontrado.");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue)
            {
                throw new Exception("O funcionário responsável pelo cadastro não foi informado.");
            }

            if (new FuncionarioNegocio().BuscarPeloUsuario(aEntidade.IdUsuarioCadastro.Value) == null)
            {
                throw new Exception("O funcionário responsável pelo cadastro não foi encontrado.");
            }
        }

        #endregion
    }
}
