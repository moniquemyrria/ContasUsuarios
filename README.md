# Aplicação Web Contas de usuários
## Aplicação básica de cadastro de usuários e endereços

###### Aplicação WEB com CRUD com as seguintes características:
###### 1. Back-End: Linguagem C# utilizando o Framework Entity
###### 2. Front-End: Linguagem JavaScript (TypeScript)  utilizando o Framework VUE, VUECli e Vuetify e NODEJs

Clone do Projeto 
---
1. Acesse: https://github.com/moniquemyrria/ContasUsuarios


Ferramentas necessárias para utilização do projeto
---
Ferramentas necessárias:
1. Microsoft Visual Studio Community
2. Microsoft Visual Studio Code
3. Microsoft SQL Server 2017

Instanciando Database da Aplicação
---
1. Importe o banco de dados CONTAS_USER que se encontra na pasta DB

ou

2. Crie o banco de dados utilizando o arquivo Script_DB_CONTAS_USER


Iniciando Back-End da Aplicação
---
1. Abra o projeto na pasta Projectback
2. Inicie o projeto 

Iniciando Front-End da Aplicação
---
1. Abra o projeto na pasta ContasUsuarios
2. Abra um terminal e instale as bibliotecas do NODEJS 

```
npm install
```

3. Inicie o projeto 

```
npm run serve
```

Estrutura da Aplicação Fron-End
---
---
#### O Padrão de Projeto utilizado é MVC (Model-View-Controller)

1. View: Telas de Listagens, Formularios e Models
```
created() {
    this._controller.carregarItens();
  }
```
2. Contracts: Funções de Comunicação Entre Tela e Controller
```
export interface IUsuarioListaView {
    showItens(list: []): void
}

export interface IUsuarioListaController {
    carregarItens(): void
}
```
3. Controllers: Funções de Comunicação entre Contracts e Banco de Dados

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


1. ModelData: Estrutura de modelo do banco de dados

<br>

```
 public class UsuarioDTO
    {
        public long id { get; set; }

        public string nome { get; set; }
        
        public List<EnderecoDTO> Endereco { get; set; }

    }
```

2. ModelView: Estrutura JSON da Tela View Front-End

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

3. Controllers: Comunicação Banco de dados e Models com os métodos (GET / PUT / POST / DELETE)

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
_Figura 01 - Diagrama Visualização da Arquitetura_

<br>

Diagrama Design da Solução
---
---
#### Modelo do diagrama notação C4:

<br>

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/documenta%C3%A7%C3%A3o/Diagram%20-%20Design%20da%20Solu%C3%A7%C3%A3o.png)
_Figura 02 - Diagrama Visualização da Arquitetura - Modelo C4_

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
_Figura 03 - Listagem de Usuários_

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
<br>

<br>

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/imagensAplicacao/02%20-%20Listagem%20de%20Usuarios%20-Pesquisa.PNG)
_Figura 04 - Listagem de Usuários - Pesquisa de Usuários_

<br>

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/imagensAplicacao/03-%20Listagem%20de%20Usuarios%20-%20Deletar%20Registro.PNG)
_Figura 05 - Listagem de Usuários - Excluír Usuários_

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/imagensAplicacao/04%20-%20Cadastro%20de%20Usuarios.PNG)
_Figura 06 - Cadastro de Usuários_

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/imagensAplicacao/05%20-%20Cadastro%20de%20Usuarios%20-%20Conulta%20CEP%20Endere%C3%A7o%20-A%C3%A7%C3%A3o.PNG)
_Figura 07 - Inserção de Endereços e consulta de CEP para o Usuário_
<br>
> Ação
<br>
> 10. Consulta de CEP base Correios Brasil 

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/main/imagensAplicacao/08%20-%20Cadastro%20de%20Usuarios%20-%20Visualiza%C3%A7%C3%A3o%20de%20Dados.PNG)
_Figura 08 - Cadastro de Usuário - Edição de Dados_

![](https://raw.githubusercontent.com/moniquemyrria/ContasUsuarios/Desenvolvimento-TemplateBase-13-03-2021/imagensAplicacao/08%20-%20Cadastro%20de%20Usuarios%20-%20Visualiza%C3%A7%C3%A3o%20de%20Dados.PNG)
_Figura 09 - Cadastro de Usuário - Visualização de Dados_


