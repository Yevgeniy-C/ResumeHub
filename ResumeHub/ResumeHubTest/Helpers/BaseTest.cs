using System;
using ResumeHub.BL.Auth;
using ResumeHub.BL.Profile;
using ResumeHub.DAL;
using Microsoft.AspNetCore.Http;
using ResumeHub.BL.General;
using ResumeHub.Deps;

namespace Resutest.Helpers
{
	public class BaseTest
	{
        protected IAuthDAL authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
        protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
		protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IProfileDAL profileDAL = new ProfileDAL();
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected IProfile profile;
        protected ISkillDAL skillDAL = new SkillDAL();

        protected CurrentUser currentUser;

        public BaseTest()
		{
            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            authBL = new Auth(authDal, encrypt, webCookie, dbSession, userTokenDAL);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL, profileDAL);
            profile = new Profile(profileDAL, skillDAL);
        }
    }
}

