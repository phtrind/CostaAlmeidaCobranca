using Dados;
using Entidade;
using Projecao;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class EventoNegocio : NegocioBase<EventoEntidade>
    {
        public override IEnumerable<EventoEntidade> ListarTodos()
        {
            return new EventoDados().ListarTodos();
        }

        public override EventoEntidade Listar(long aCodigo)
        {
            return new EventoDados().Listar(aCodigo);
        }

        public IEnumerable<ComboProjecao> getComboEventos()
        {
            return new EventoDados().getComboEventos();
        }

        public override void ValidateRegister(EventoEntidade aEntidade)
        {
            if (string.IsNullOrEmpty(aEntidade.Nome))
            {
                throw new Exception("É obrigatório informar o nome do evento.");
            }

            if (aEntidade.Data == default(DateTime))
            {
                throw new Exception("A data do evento informada é inválida.");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue)
            {
                throw new Exception("É obrigatório informar o usuário resposável pelo cadastro.");
            }

            VerificarChaves(aEntidade);
        }

        private void VerificarChaves(EventoEntidade aEntidade)
        {
            if (!new EventoDados().ValidarChaves(aEntidade))
            {
                throw new Exception("Já existe um evento com esse nome e essa data cadastrado.");
            }
        }
    }
}
