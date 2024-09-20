using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Middleware;
using ResumeHub.BL;
using ResumeHub.BL.Auth;
using ResumeHub.ViewMapper;
using ResumeHub.ViewModels;

namespace ResumeHub.Controllers
{
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IAuth authBl;

        public LoginController(IAuth authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Index()
        {
            return View("Index", new LoginViewModel());
        }

        [HttpPost]
        [Route("/login")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                    return Redirect("/");
                }
                catch (ResumeHub.BL.AuthorizationException)
                {
                    ModelState.AddModelError("Email", "Имя или Email неверные");
                }
            }

            return View("Index", model);
        }
    }
}

