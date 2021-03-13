using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryKodigos;
using LibraryKodigos.KActiveDirectory;
using LibraryKodigos.KEmails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace projectback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Email")]
        public KRetorno SendEmail()
        {
            var list = new List<KUserEmail>
            {
                new KUserEmail() { Email = "pedro.pequeno@kodigos.com.br" },
                new KUserEmail() { Email = "monique.rocha@kodigos.com.br" },
                new KUserEmail() { Email = "eranio.leite@kodigos.com.br" }
            };

            KModelEmail kModelEmail = new KModelEmail
            {
                EmailFrom = "xxxx",
                From = new KUserEmail() { Email = "xxxxx", Pass = "xxxxx" },
                To = list,
                Body = "<h1>TESTE HTML</h1><b>Teste 1</b>",
                IsHtml = true,
                STMP = "smtp.gmail.com",
                Port = 587,
                Subject = "Teste 1 bla"
            };


            return KSendEmail.SendEmail(kModelEmail);
        }
    }
}
