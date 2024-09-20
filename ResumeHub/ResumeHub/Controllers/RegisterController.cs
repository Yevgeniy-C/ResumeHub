using System;
using ResumeHub.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using ResumeHub.ViewModels;
using ResumeHub.ViewMapper;
using ResumeHub.BL;
using MyBlog.Middleware;

namespace ResumeHub.Controllers
{
    [SiteNotAuthorize()]
    public class RegisterController: Controller
	{
		private readonly IAuth authBl;

        public RegisterController(IAuth authBl)
		{
			this.authBl = authBl;
        }

		[HttpGet]
		[Route("/register")]
		public IActionResult Index()
		{
			return View("Index", new RegisterViewModel());
		}

        [HttpPost]
        [Route("/register")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await authBl.Register(AuthMapper.MapRegisterViewModelToUserModel(model));
                    return Redirect("/");
                }
                catch (DuplicateEmailException)
                {
                    ModelState.TryAddModelError("Email", "Email already exist");
                }
            }

            return View("Index", model);
        }
    }
}

