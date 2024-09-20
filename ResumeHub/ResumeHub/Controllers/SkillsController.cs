using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Middleware;
using ResumeHub.BL.Auth;
using ResumeHub.BL.Profile;
using ResumeHub.ViewModels;
using ResumeHub.ViewMapper;
using ResumeHub.DAL.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResumeHub.Controllers
{
    [SiteAuthorize()]
    public class SkillsController : Controller
    {
        private readonly ISkill skill;
        private readonly ICurrentUser currentUser;
        private readonly IProfile profile;

        public SkillsController(ISkill skill, ICurrentUser currentUser, IProfile profile)
        {
            this.skill = skill;
            this.currentUser = currentUser;
            this.profile = profile;
        }

        public async Task<IActionResult> My()
        {
            var p = await currentUser.GetProfiles();
            var myskills = await profile.GetProfileSkills(p.FirstOrDefault()?.ProfileId ?? 0);
            return new JsonResult(myskills.Select(m => new SkillViewModel{ Name = m.SkillName, Level = m.Level }));
        }

        [HttpPut]
        public async Task<IActionResult> Add([FromBody] SkillViewModel skill)
        {
            var p = await currentUser.GetProfiles();
            ProfileSkillModel profileSkillModel = SkillMapper.MapSkillViewModelToProfileSkillModel(skill);
            profileSkillModel.ProfileId = p.FirstOrDefault()?.ProfileId ?? 0;
            await profile.AddProfileSkill(profileSkillModel);
            return Ok();
        }

        [Route("skills/search/{search}")]
        public async Task<IActionResult> Search(string search)
        {
            var skills = await this.skill.Search(5, search);
            return new JsonResult(skills?.Select(m => m.SkillName).ToList());
        }
    }
}

