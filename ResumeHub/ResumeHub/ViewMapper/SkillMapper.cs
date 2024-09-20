using System;
using ResumeHub.DAL.Models;
using ResumeHub.ViewModels;

namespace ResumeHub.ViewMapper
{
	public static class SkillMapper
	{
        public static ProfileSkillModel MapSkillViewModelToProfileSkillModel(SkillViewModel model)
        {
            return new ProfileSkillModel()
            {
                 SkillName = model.Name,
                 Level = model.Level
            };
        }
    }
}

