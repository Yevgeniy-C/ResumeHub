using System;
using System.ComponentModel.DataAnnotations;
using ResumeHub.DAL.Models;

namespace ResumeHub.BL.Auth
{
	public interface IAuth
	{
		Task<int> CreateUser(ResumeHub.DAL.Models.UserModel user);

		Task<int> Authenticate(string email, string password, bool rememberMe);

		Task ValidateEmail(string email);

		Task Register(UserModel user);
    }
}

