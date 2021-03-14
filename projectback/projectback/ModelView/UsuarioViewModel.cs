using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectback.ModelView
{
    public class UsuarioViewModel
    {
  
        public string nome { get; set; }
        public string email { get; set; }
        public string confirmeEmail { get; set; }
        public string senha { get; set; }
        public string confirmaSenha { get; set; }
        public string dataNascimento { get; set; }
        public string cpf { get; set; }
        public string deletado { get; set; }
        public DateTime dataCadastro { get; set; }
        public List<Endereco> endereco { get; set; }
    }

    public class Endereco
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }
        public string tipo { get; set; }
        public string numero { get; set; }
    }

}
