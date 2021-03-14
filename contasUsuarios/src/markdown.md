# Aplicação Web Contas de usuários
## Aplicação básica de cadastro de usuários e endereços

###### Alicação WEB com CRUD com as seguintes características:
###### 1. Back-End: Linguagem C# utilizando o Framework Entity
###### 2. Front-End: Linguagem JavaScript (TypeScript)  utilizando o Framework VUE, VUECli e Vuetify e NODEJs


<br>
Clone do Projeto 
---
- Acesse: https://github.com/moniquemyrria/ContasUsuarios


<br>
Ferramentas necessárias para utilização do projeto
---
Ferramentas necessárias:
- Microsoft Visual Studio Community
- Microsft Visual Studio Code
- Microsoft SQL Server 2017

<br>
Instanciando Database da Aplicação
---
- Importe o banco de dados CONTAS_USER que se encontra na pasta DB
<br>
ou
<br>
- Crie o banco de dados utilizando o arquivo Script_DB_CONTAS_USER

<br>
Iniciando Back-End da Aplicação
---
- Abra o projeto na pasta Projectback
- Inicie o projeto 

<br>
Iniciando Front-End da Aplicação
---
- Abra o projeto na pasta ContasUsuarios
- Abra um terminal e instale as bibliotecas do NODEJS 
<br>
``
npm install
``

- Inicie o projeto 
<br>
``
npm run serve
``

Estrutura da Aplicação Fron-End
---
---
#### O Padrão de Projeto utilizado é MVC (Model-View-Controller)
<br>

- View: Telas de Listagens, Formularios e Models
```
created() {
    this._controller.carregarItens();
  }
```
- Contracts: Funções de Comunicação Entre Tela e Controller
```
export interface IUsuarioListaView {
    showItens(list: []): void
}

export interface IUsuarioListaController {
    carregarItens(): void
}
```
- Controllers: Funções de Comunicação entre Contracts e Banco de Dados

```
    carregarItens(): void {
        this._view.loading(true)

        axios
            .get('Usuario')
            .then((response: any) => {
                this._view.showItens(response.data)
            }).catch((e: any) => {
            })
    }
```
Estrutura da Aplicação Back-End
---
---
#### O Padrão de Projeto utilizado é MVC (Model-View-Controller)


- ModelData: Estrutura de modelo do banco de dados

<br>

```
 public class UsuarioDTO
    {
        public long id { get; set; }

        public string nome { get; set; }
        
        public List<EnderecoDTO> Endereco { get; set; }

    }
```

- ModelView: Estrutura JSON da Tela View Front-End

<br>

```
public class UsuarioViewModel
    {
  
        public string nome { get; set; }
        public List<Endereco> endereco { get; set; }
    }

    public class Endereco
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
    
    }
```

- Controllers: Comunicação Banco de dados e Models com os métodos (GET / PUT / POST / DELETE)

<br>

```
     [HttpGet("{cep}")]
        public async Task<CepModelViewDTO> GetCEP(string cep)
        {
            return await ConsultaCep(cep);
        }
        private async Task<CepModelViewDTO> ConsultaCep(string cep)
        {
            ...
        }
```

Layout da Aplicação
---
---
#### Telas e Menus
<br>


















