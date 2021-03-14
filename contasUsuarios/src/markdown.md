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
Diagrama de Visualização da Arquitetura
---
---
#### Modelo do diagrama notação C4

<br>

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/documenta%C3%A7%C3%A3o/Diagram%20-%20Visualiza%C3%A7%C3%A3o%20da%20Arquitetura.png)
_Figura 01 - Diagrama Visualização da Arquitetura

<br>

Diagrama Design da Solução
---
---
#### Modelo do diagrama notação C4:

<br>

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/documenta%C3%A7%C3%A3o/Diagram%20-%20Design%20da%20Solu%C3%A7%C3%A3o.png)
_Figura 01 - Diagrama Visualização da Arquitetura - Modelo C4_

Layout da Aplicação
---
---
#### Aplicação possui telas auto explicativas com:
- Menu de Opções ao usuário lado esquerdo
- Menu Hamburguer (Retirada de visualziação do menu) na barra superior de Informações
- Barra superior com informações da Aplicação
- Tela de Listagem e Formulários
- Botões de Ações
- Grids para Listagens

<br>

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/Desenvolvimento-TemplateBase-13-03-2021/imagensAplicacao/01%20-%20Listagem%20de%20Usuarios%20-A%C3%A7%C3%B5es.png)
_Figura 02 - Listagem de Usuários_

> Ações:
>1. Menu Hamburger
>2. Opção do Menu Usuários
>3. Informações da desenvolvedora da aplicação
>4. Botão de ação Novo
>5. Pesquisa de Usuários
>6. Botão de ação Editar
>7. Botão de ação Excluír
>8. Botão de ação Visualizar dados
>9. Paginação do grid de dados






















