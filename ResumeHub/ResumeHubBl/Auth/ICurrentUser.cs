using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.BL.Auth
{
	public interface ICurrentUser
	{
		Task<bool> IsLoggedIn();

		Task<int?> GetCurrentUserId();

		Task<IEnumerable<ProfileModel>> GetProfiles();
    }
}

