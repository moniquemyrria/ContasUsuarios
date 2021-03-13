using LibraryKodigos.KUtils;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace LibraryKodigos.KActiveDirectory
{
    public class KLoginActiveDirectory
    {
        public static KRetorno Login(KModelActiveDirectory kModelActiveDirectory)
        {
            KRetorno retorno = new KRetorno();
            try
            {
                DirectoryEntry directoryEntry = new DirectoryEntry(kModelActiveDirectory.Dominio,
                    kModelActiveDirectory.kUserActiveDirectory.User,
                    kModelActiveDirectory.kUserActiveDirectory.Pass);

                DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.Filter = "(SAMAccountName=" + kModelActiveDirectory.kUserActiveDirectory.User + ")";
                SearchResult searchResult = directorySearcher.FindOne();
                if ((Int32)searchResult.Properties["userAccountControl"][0] == 512)
                {
                    retorno.Sucesso = true;
                    retorno.Message = "Usuário Autenticado!";
                }
                else
                {
                    retorno.Sucesso = false;
                    retorno.Message = "ERRO: Usuário/Senha Inválido!";
                }
            }
            catch (Exception e)
            {
                retorno = KDesignException.GetException(e);
            }
            return retorno;
        }
    }
}
