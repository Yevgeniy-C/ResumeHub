using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.DAL
{
	public interface ISkillDAL
	{
        Task<int> Create(string model);

        Task<IEnumerable<SkillModel>?> Search(int top, string skillname);

        Task<SkillModel> Get(string skillname);

        Task<IEnumerable<ProfileSkillModel>> GetProfileSkills(int profileId);

        Task<int> AddProfileSkill(ProfileSkillModel model);
    }
}

