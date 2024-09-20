﻿using System;
using ResumeHub.BL.General;
using ResumeHub.BL.Profile;
using ResumeHub.DAL;
using ResumeHub.DAL.Models;

namespace ResumeHub.BL.Auth
{
	public class CurrentUser: ICurrentUser
	{
        private readonly IDbSession dbSession;
        private readonly IWebCookie webCookie;
        private readonly IUserTokenDAL userTokenDAL;
		private readonly IProfileDAL profileDAL;

        public CurrentUser(
            IDbSession dbSession,
            IWebCookie webCookie,
            IUserTokenDAL userTokenDAL,
			IProfileDAL profileDAL
            )
		{
			this.dbSession = dbSession;
			this.webCookie = webCookie;
			this.userTokenDAL = userTokenDAL;
			this.profileDAL = profileDAL;
        }

		public async Task<int?> GetUserIdByToken()
		{
			string? tokenCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
			if (tokenCookie == null)
				return null;
			Guid? tokenGuid = Helpers.StringToGuidDef(tokenCookie ?? "");
			if (tokenGuid == null)
				return null;

            int? userid = await userTokenDAL.Get((Guid)tokenGuid);
			return userid;
        }

		public async Task<bool> IsLoggedIn()
		{
			bool isLoggedIn = await dbSession.IsLoggedIn();
			if (!isLoggedIn)
			{
				int? userid = await GetUserIdByToken();
				if (userid != null)
				{
					await dbSession.SetUserId((int)userid);
					isLoggedIn = true;
                }
            }
			return isLoggedIn;
        }

		public async Task<int?> GetCurrentUserId()
		{
			return await dbSession.GetUserId();
        }

		public async Task<IEnumerable<ProfileModel>> GetProfiles() {
            int? userid = await GetCurrentUserId();
            if (userid == null)
                throw new Exception("User not found");

            return await profileDAL.GetByUserId((int)userid);
        }
    }
}

