using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projectback.ModelsData
{
    public class EnderecoDTO
    {
        public long id { get; set; }

        public long idUsuario { get; set; }
        
        public string cep { get; set; }
        public string logradouro { get; set; }
        
        public string bairro { get; set; }

        public string localidade { get; set; }

        public string uf { get; set; }

        public string tipo { get; set; }

        public string numero { get; set; }
        public string complemento { get; set; }

        [JsonIgnore]
        public UsuarioDTO UsuarioDTO { get; set; }

}
}
