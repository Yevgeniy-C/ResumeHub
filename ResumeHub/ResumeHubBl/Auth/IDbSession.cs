﻿using System;
using ResumeHub.DAL.Models;

namespace ResumeHub.BL.Auth
{
    public interface IDbSession
    {
        Task<SessionModel> GetSession();

        Task SetUserId(int userId);

        Task<int?> GetUserId();

        Task<bool> IsLoggedIn();

        Task Lock();

        void ResetSessionCache();

        Task UpdateSessionData();

        void AddValue(string key, object value);

        void RemoveValue(string key);

        object GetValueDef(string key, object defaultValue);
    }
}

