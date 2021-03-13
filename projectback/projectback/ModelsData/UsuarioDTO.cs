using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectback.ModelsData
{
    public class UsuarioDTO
    {
        public long id { get; set; }

        public string nome { get; set; }
        
        public string email { get; set; }
        
        public string senha { get; set; }
        
        public DateTime dataCadastro { get; set; }

        public string deletado { get; set; }

    }
}
