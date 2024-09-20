using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.BL.Resume
{
	public interface IResume
	{
        Task<IEnumerable<ProfileModel>> Search(int top);

        Task<ResumeModel> Get(int profileId);
    }
}

