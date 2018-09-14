using Dapper;
using Entidade;
using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projecao;
using Dommel;

namespace Dados
{
    public class EventoDados : DadosBase<EventoEntidade>
    {
        public IEnumerable<dynamic> getComboEventos()
        {
            return db.Query($@"SELECT EVE_CODIGO, 
                                               EVE_NOME
                                        FROM EVE_EVENTOS
                                        ORDER BY EVE_NOME;");
        }

        public bool ValidarChaves(EventoEntidade aEntidade)
        {
            var evento = db.Select<EventoEntidade>(x => x.Nome == aEntidade.Nome && x.Data == aEntidade.Data).FirstOrDefault();

            return evento == null;
        }

        public IEnumerable<dynamic> Relatorio()
        {
            return db.Query($@"SELECT E.EVE_CODIGO, 
                                      E.EVE_NOME, 
                                      E.EVE_DATA, 
                                      CONCAT(EN.END_CIDADE, ' / ', EN.END_ESTADO) AS LOCALIDADE
                               FROM EVE_EVENTOS E
                                    INNER JOIN END_ENDERECOS EN ON E.END_CODIGO = EN.END_CODIGO;");
        }

        public dynamic RelatorioDetalhado(long aIdEvento)
        {
            return db.QueryFirstOrDefault($@"SELECT E.EVE_CODIGO, 
                                      E.EVE_NOME, 
                                      E.EVE_DATA, 
                                      EN.END_CODIGO, 
                                      EN.END_CEP, 
                                      EN.END_LOGRADOURO, 
                                      EN.END_NUMERO, 
                                      EN.END_COMPLEMENTO, 
                                      EN.END_BAIRRO, 
                                      EN.END_ESTADO, 
                                      EN.END_CIDADE
                               FROM EVE_EVENTOS E
                                    INNER JOIN END_ENDERECOS EN ON E.END_CODIGO = EN.END_CODIGO
                               WHERE E.EVE_CODIGO = {aIdEvento};");
        }
    }
}
