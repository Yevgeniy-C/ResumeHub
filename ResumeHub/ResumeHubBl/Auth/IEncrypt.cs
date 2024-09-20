using System;
namespace ResumeHub.BL.Auth
{
	public interface IEncrypt
	{
		string HashPassword(string password, string salt);
	}
}

