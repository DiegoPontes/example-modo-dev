# Aplicação exemplo para autenticação

Exemplo de configuração de vários provedores de autenticação para aplicações .NET 4.8.

## Passos necessários para autenticação em uma nova aplicação:

1 - Adicionar os pacotes nuget adicionais (vide seção mais abaixo);
2 - Copiar o diretório `Auth` do projeto web
3 - Copiar o arquivo Startup.cs realizando as devidas modificações;
4 - Configurar o web.config;
5 - Copiar as imagens do diretório `Images`
6 - Copiar os arquivos css: `Site.css`, `br-luf.css`, `sp-idp.css`
7 - Configurar chamadas de autenticação conforme indicado no arquivo `AccountController` e na view `Account/Login`

## Parâmetros do `web.config`

* owin:AutomaticAppStartup - parâmetro deve estar com valor "true";
* owin:AppStartup parâmetro deve conter o nome completo da classe Startup;
* app:CookiePath - deve ser o mesmo do domínio da aplicação (exemplo: para https//localhost/MINHA_APLICACAO, colocar: value="/MINHA_APLICACAO/" />


### Sefaz Identity

* si:Realm - endereço da aplicação cadastrado no SEFAZ Identity;
* si:Reply - url de retorno na operação de login;
* si:Metadata - url de metadados do SEFAZ Identity

OBS: informações sobre o SEFAZ Identity podem ser encontradas [aqui](https://ads.intra.fazenda.sp.gov.br/tfs/ADMIN/Wiki_Arquitetura/_wiki/wikis/Wiki_Arquitetura.wiki/91/Sefaz-Identity).

#### Principais classes

`/Auth/SefazIdentity/SefazIdentityProvider.cs` - arquivo com os eventos utilizados para comunicação com o Sefaz Identity. 

Pode ser alterado para:
* buscar escopos;
* incluir claims de interesse da aplicação;
* realizar ações adicionais (validações por exemplo).

### Login único federal (Gov.Br)

* luf:ClaimsKey - constante do tipo de autenticação (não alterar)
* luf:ClientId - deve ter o valor do client_id da aplicação cadastrado no Gov.Br
* luf:ClientSecret - deve ter o valor do client_secret da aplicação cadastrado no Gov.Br
* luf:Authority - url do autenticador do Gov.Br:
	* Ambiente de homologação: https://sso.staging.acesso.gov.br
	* Ambiente de produção: https://sso.acesso.gov.br
* luf:ApiUri - url do serviço de disponibilidades e escopos do Gov.Br
	* Ambiente de homologação: https://sso.staging.acesso.gov.br
	* Ambiente de produção: https://sso.acesso.gov.br
* luf:ClaimsScope - escopos que devem ser retornados pelo Gov.Br. A lista de escopos pode ser consultada no array scopes_supported do endereço: https://sso.acesso.gov.br/.well-known/openid-configuration
* luf:LogoutUri - url de direcionamento em caso de logout (deve estar cadastrada no Gov.Br);
* luf:RedirectUri - url de direcionamento em caso login (deve estar cadastrada no Gov.Br)

#### Principais classes

`/Auth/Luf/LufProvider.cs` - arquivo com os eventos utilizados para comunicação com o Gov.Br. 

Pode ser alterado para:
* buscar escopos;
* incluir claims de interesse da aplicação;
* realizar ações adicionais (validações por exemplo).

## Pacotes nuget adicionais necessários:

* Microsoft.AspNet.Identity.Core
* Microsoft.AspNet.Identity.Owin
* Microsoft.Extensions.Configuration.Abstractions
* Microsoft.Extensions.DependencyInjection.Abstractions
* Microsoft.IdentityModel
* Microsoft.IdentityModel.Abstractions
* Microsoft.IdentityModel.JsonWebTokens
* Microsoft.IdentityModel.Protocols
* Microsoft.IdentityModel.Protocols.OpenIdConnect
* Microsoft.IdentityModel.Protocols.WsFederation
* Microsoft.IdentityModel.Tokens
* Microsoft.IdentityModel.Tokens.Saml
* Microsoft.IdentityModel.Xml
* Microsoft.Owin
* Microsoft.Owin.Host.SystemWeb
* Microsoft.Owin.Security
* Microsoft.Owin.Security.Cookies
* Microsoft.Owin.Security.OAuth
* Microsoft.Owin.Security.OpenIdConnect
* Microsoft.Owin.Security.WsFederation
* Owin
* System.IdentityModel.Tokens.Jwt
* System.IdentityModel.Tokens.ValidatingIssuerNameRegistry



