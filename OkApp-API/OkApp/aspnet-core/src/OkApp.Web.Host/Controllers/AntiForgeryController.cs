using Microsoft.AspNetCore.Antiforgery;
using OkApp.Controllers;

namespace OkApp.Web.Host.Controllers
{
    public class AntiForgeryController : OkAppControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

