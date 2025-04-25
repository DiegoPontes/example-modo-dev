using Microsoft.Owin.Security;
using modelo_net_autenticacao.Auth;
using modelo_net_autenticacao.Auth.Base;
using modelo_net_autenticacao.Auth.SefazIdentity;
using System.Web;
using System.Web.Mvc;

namespace modelo_net_autenticacao.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        public void LoginUnicoFederal()
        {
            if (!Request.IsAuthenticated)
            {
                Request.RequestContext.HttpContext.Session.Clear();
                Request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = new OidcConfigOptions(AuthProviderPrefix.LUF).RedirectUri },
                    AuthProviderPrefix.LUF);
            }
        }

        public void IdpSp()
        {
            if (!Request.IsAuthenticated)
            {
                Request.RequestContext.HttpContext.Session.Clear();
                Request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = new OidcConfigOptions(AuthProviderPrefix.IDPSP).RedirectUri },
                    AuthProviderPrefix.IDPSP);
            }
        }

        public void LoginSEFAZIdentityPF()
        {
            if (!Request.IsAuthenticated)
            {
                Request.RequestContext.HttpContext.Session.Clear();
                Request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = SefazIdentityConfigOptions.Reply },
                    TipoUsuarioAutenticado.PESSOA_FISICA);
            }
        }

        public void LoginSEFAZIdentityPJ()
        {
            if (!Request.IsAuthenticated)
            {
                Request.RequestContext.HttpContext.Session.Clear();
                Request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = SefazIdentityConfigOptions.Reply },
                    TipoUsuarioAutenticado.PESSOA_JURIDICA);
            }
        }

        public void LoginSEFAZIdentityFaz()
        {
            if (!Request.IsAuthenticated)
            {
                Request.RequestContext.HttpContext.Session.Clear();
                Request.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = SefazIdentityConfigOptions.Reply },
                    TipoUsuarioAutenticado.FAZENDARIO);
            }

        }
    }
}