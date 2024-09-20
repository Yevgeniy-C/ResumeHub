using ResumeHub.ViewModels;
using ResumeHub.DAL.Models;

namespace ResumeHub.ViewMapper
{
	public static class AuthMapper
	{
		public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
		{
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}

