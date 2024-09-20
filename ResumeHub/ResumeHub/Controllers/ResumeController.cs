using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResumeHub.BL.Resume;

namespace ResumeHub.Controllers
{
    public class ResumeController : Controller
    {
        private readonly IResume resume;

        public ResumeController(IResume resume)
        {
            this.resume = resume;
        }

        [Route("/resume/{profileid}")]
        public async Task<IActionResult> Index(int profileid)
        {
            var model = await resume.Get(profileid);
            return View(model);
        }
    }
}

