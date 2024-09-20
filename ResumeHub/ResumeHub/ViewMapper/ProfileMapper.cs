using ResumeHub.BL.Profile;
using ResumeHub.DAL.Models;
using ResumeHub.ViewModels;

namespace ResumeHub.ViewMapper
{
	public static class ProfileMapper
	{
        public static ProfileModel MapProfileViewModelToProfileModel(ProfileViewModel model)
        {
            return new ProfileModel()
            {
                ProfileId = model.ProfileId,
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileStatus = model.ProfileStatus
            };
        }

        public static ProfileViewModel MapProfileModelToProfileViewModel(ProfileModel model)
        {
            return new ProfileViewModel()
            {
                ProfileId = model.ProfileId,
                ProfileName = model.ProfileName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileImage = model.ProfileImage,
                ProfileStatus = model.ProfileStatus
            };
        }
    }
}

