using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Dapper;
using LibraryMR;
using LibraryMR.KUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using projectback.Context;
using projectback.ModelsData;



namespace projectback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultaCepController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _config;


        public ConsultaCepController(DatabaseContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("{cep}")]
        public async Task<CepModelViewDTO> GetCEP(string cep)
        {
            return await ConsultaCep(cep);
        }
        private async Task<CepModelViewDTO> ConsultaCep(string cep)
        {
            HttpClient client;
            Uri usuarioUri;
            client = new HttpClient();
            string path = _config.GetSection("LinkApiCep:path").Value.ToString(); ;
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(cep +"/json/").Result;
            int contador = 0;
            //se retornar com sucesso busca os dados
            if (response.IsSuccessStatusCode)
            {
                //pegando o cabeçalho
                usuarioUri = response.Headers.Location;

                //Pegando os dados do Rest e armazenando na variável usuários
                var usuariosJson = response.Content.ReadAsStringAsync();
                CepModelViewDTO cepModelViewDTO = JsonConvert.DeserializeObject<CepModelViewDTO>(usuariosJson.Result);

               
                return cepModelViewDTO;
            }
            return null;
        }

        public class CepModelViewDTO
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
        }



    }
}
