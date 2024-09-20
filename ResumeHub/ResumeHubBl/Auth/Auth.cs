using ResumeHub.DAL.Models;
using ResumeHub.DAL;
using ResumeHub.BL.General;

namespace ResumeHub.BL.Auth
{
	public class Auth: IAuth
	{
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;
        private readonly IUserTokenDAL userTokenDAL;
        private readonly IWebCookie webCookie;


        public Auth(IAuthDAL authDal,
            IEncrypt encrypt,
            IWebCookie webCookie,
            IDbSession dbSession,
            IUserTokenDAL userTokenDAL)
		{
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
            this.userTokenDAL = userTokenDAL;
            this.webCookie = webCookie;
        }

        public async Task Login(int id)
        {
            await dbSession.SetUserId(id);
        }

        public async Task<int> Authenticate(string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);

            if (user.UserId != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.UserId ?? 0);

                if (rememberMe)
                {
                    Guid tokenId = await userTokenDAL.Create(user.UserId ?? 0);
                    this.webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
                }

                return user.UserId ?? 0;
            }
            throw new AuthorizationException();
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);

            int id = await authDal.CreateUser(user);
            await Login(id);
            return id;
        }

        public async Task ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null)
                throw new DuplicateEmailException();
        }

        public async Task Register(UserModel user)
        {
            using (var scope = Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(user.Email);
                await CreateUser(user);
                scope.Complete();
            }
        }
    }
}

