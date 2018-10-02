using Dapper;
using Entidade;
using System.Collections.Generic;

namespace Dados
{
    public class CrmDados : DadosBase<CrmEntidade>
    {
        public IEnumerable<dynamic> BuscarPorContrato(long aId)
        {
            return db.Query($@"SELECT C.CRM_CODIGO, 
                                      C.CRM_DESCRICAO, 
                                      C.CRM_DTHCADASTRO, 
                                      F.FUN_NOME
                               FROM CRM_CRMCONTRATOS C
                                    INNER JOIN FUN_FUNCIONARIOS F ON C.USU_CADASTRO = F.USU_CODIGO
                               WHERE C.CON_CODIGO = {aId};");
        }
    }
}
