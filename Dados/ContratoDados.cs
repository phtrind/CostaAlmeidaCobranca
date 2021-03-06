﻿using Dapper;
using Entidade;
using System;
using System.Collections.Generic;

namespace Dados
{
    public class ContratoDados : DadosBase<ContratoEntidade>
    {
        public IEnumerable<dynamic> BuscarParaEditar(long idContrato)
        {
            return db.Query($@"SELECT C.CON_CODIGO, 
                                      C.CON_VENDEDOR, 
                                      C.CON_COMPRADOR, 
                                      C.CON_ANIMAL, 
                                      C.CON_OBSERVACAO, 
                                      C.EVE_CODIGO, 
                                      C.CON_VALOR, 
                               (
                                   SELECT COUNT(*)
                                   FROM PAR_PARCELAS
                                   WHERE CON_CODIGO = C.CON_CODIGO
                               ) AS QUANT_PARCELAS, 
                                      P.PAR_CODIGO, 
                                      P.PAR_VALOR, 
                                      P.PAR_DTHVENCIMENTO, 
                                      P.PAR_TAXALUCRO
                               FROM CON_CONTRATOS C
                                    INNER JOIN PAR_PARCELAS P ON C.CON_CODIGO = P.CON_CODIGO
                               WHERE C.CON_CODIGO = {idContrato};");
        }

        public IEnumerable<dynamic> Relatorio()
        {
            return db.Query($@"SELECT C.CON_CODIGO, 
                               (
                                   SELECT CLI_NOME
                                   FROM CLI_CLIENTES
                                   WHERE CLI_CODIGO = C.CON_VENDEDOR
                               ) AS VENDEDOR, 
                               (
                                   SELECT CLI_NOME
                                   FROM CLI_CLIENTES
                                   WHERE CLI_CODIGO = C.CON_COMPRADOR
                               ) AS COMPRADOR, 
                               E.EVE_NOME, 
                               C.CON_VALOR, 
                               C.CON_STATUS, 
                               (
                                   SELECT COUNT(*)
                                   FROM PAR_PARCELAS
                                   WHERE CON_CODIGO = C.CON_CODIGO
                               ) AS PARCELAS
                               FROM CON_CONTRATOS C
                                    LEFT JOIN EVE_EVENTOS E ON C.EVE_CODIGO = E.EVE_CODIGO;");
        }

        public void AtualizarValorTotal(long aId)
        {
            db.Execute($@"UPDATE CON_CONTRATOS
                             SET 
                                 CON_VALOR =
                           (
                               SELECT SUM(PAR_VALOR)
                               FROM PAR_PARCELAS
                               WHERE CON_CODIGO = {aId}
                           )
                           WHERE CON_CODIGO = {aId};");
        }

        public override bool Excluir(ContratoEntidade aObjeto)
        {
            db.Execute($"DELETE FROM PAR_PARCELAS WHERE CON_CODIGO = {aObjeto.Id.Value}");
            db.Execute($"DELETE FROM CON_CONTRATOS WHERE CON_CODIGO = {aObjeto.Id.Value}");

            return true;
        }
    }
}
