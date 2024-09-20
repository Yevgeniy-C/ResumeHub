using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ResumeHub.BL.Auth;
using ResumeHub.BL.Resume;
using ResumeHub.DAL;

namespace ResumeHub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly ICurrentUser currentUser;
    private readonly IResume resume;

    public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser, IResume resume)
    {
        this.logger = logger;
        this.currentUser = currentUser;
        this.resume = resume;
    }

    public async Task<IActionResult> Index()
    {
        var latestResumes = await resume.Search(4);
        return View(latestResumes);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}

