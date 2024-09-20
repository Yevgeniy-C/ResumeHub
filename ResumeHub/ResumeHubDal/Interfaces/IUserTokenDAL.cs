using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.DAL
{
	public interface IUserTokenDAL
	{
        Task<Guid> Create(int userId);

        Task<int?> Get(Guid tokenid);
    }
}

