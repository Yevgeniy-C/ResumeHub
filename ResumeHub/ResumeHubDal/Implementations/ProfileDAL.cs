﻿using ResumeHub.DAL.Models;

namespace ResumeHub.DAL
{
    public class ProfileDAL : IProfileDAL
    {
        public async Task<int> Add(ProfileModel profile)
        {
            string sql = @"insert into Profile(UserId, ProfileName, FirstName, LastName, ProfileImage, ProfileStatus)
                    values(@UserId, @ProfileName, @FirstName, @LastName, @ProfileImage, @ProfileStatus) returning ProfileId";
            return await DbHelper.QueryScalarAsync<int>(sql, profile);
        }

        public async Task<IEnumerable<ProfileModel>> GetByUserId(int userId)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage, ProfileStatus
                        from Profile
                        where UserId = @id", new { id = userId });
        }

        public async Task<ProfileModel> GetByProfileId(int profileId)
        {
            return await DbHelper.QueryScalarAsync<ProfileModel>(@"
                        select 	ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage, ProfileStatus
                        from Profile
                        where ProfileId = @profileId", new { profileId = profileId });
        }

        public async Task Update(ProfileModel profile)
        {
            string sql = @"Update Profile
                    set ProfileName = @ProfileName,
                        FirstName = @FirstName,
                        LastName = @LastName,
                        ProfileImage = @ProfileImage,
                        ProfileStatus = @ProfileStatus
                    where ProfileId = @ProfileId";
            await DbHelper.ExecuteAsync(sql, profile);
        }

        public async Task<IEnumerable<ProfileModel>> Search(int top)
        {
            return await DbHelper.QueryAsync<ProfileModel>(@$"
                        select ProfileId, UserId, ProfileName, FirstName, LastName, ProfileImage 
                        from Profile
                        where ProfileStatus = @profileStatus
                        order by 1 desc
                        limit @top 
                        ", new { top = top, profileStatus = ProfileStatusEnum.Public });
        }
    }
}

