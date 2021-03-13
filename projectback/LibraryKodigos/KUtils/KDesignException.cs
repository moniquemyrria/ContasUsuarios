using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryKodigos.KUtils
{
    public class KDesignException
    {   
        public static KRetorno GetException(Exception e)
        {
            KRetorno retorno = new KRetorno
            {
                Sucesso = false
            };

            if (e.InnerException != null)
            {
                retorno.Message = e.InnerException.ToString();
            }
            else
            {
                retorno.Message = e.Message;
            }
            return retorno;
        }
    }
}
