using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryKodigos.KActiveDirectory
{
    public class KModelActiveDirectory
    {
        public string Dominio { get; set; }
        public KUserActiveDirectory kUserActiveDirectory { get; set; }
    }
}
