﻿using ResumeHub.DAL.Models;

namespace ResumeHub.DAL
{
	public class AuthDAL: IAuthDAL
	{
        public async Task<UserModel> GetUser(string email)
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                    select UserId, Email, Password, Salt, Status
                    from AppUser
                    where Email = @email", new { email = email });
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var result = await DbHelper.QueryAsync<UserModel>(@"
                        select UserId, Email, Password, Salt, Status
                        from AppUser
                        where UserId = @id", new { id = id });
            return result.FirstOrDefault() ?? new UserModel();
        }

        public async Task<int> CreateUser(UserModel model)
        {
            string sql = @"insert into AppUser(Email, Password, Salt, Status)
                    values(@Email, @Password, @Salt, @Status) returning UserId";
            return await DbHelper.QueryScalarAsync<int>(sql, model);
        }
    }
}

