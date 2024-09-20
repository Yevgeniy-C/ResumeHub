using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.DAL
{
	public interface IAuthDAL
	{
		Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        Task<int> CreateUser(UserModel model);
    }
}

