using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.BL.Profile
{
	public interface ISkill
	{
        Task<IEnumerable<SkillModel>?> Search(int top, string skillname);

    }
}

