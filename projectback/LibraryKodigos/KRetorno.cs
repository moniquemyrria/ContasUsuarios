using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMR
{
    public class KRetorno<T> : KRetorno
        where T : class
    {
        public T TRetorno { get; set; }
    }

    public class KRetorno
    {
        public bool Sucesso { get; set; }
        public string Message { get; set; }
    }
}
