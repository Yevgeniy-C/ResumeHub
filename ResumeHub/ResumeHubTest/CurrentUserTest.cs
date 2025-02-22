﻿using System;
using ResumeHub.BL;
using Resutest.Helpers;
using System.Transactions;
using ResumeHub.BL.Auth;

namespace Resutest
{
	public class CurrentUserTest : Helpers.BaseTest
    {
        [Test]
        public async Task BaseRegistrationTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await CreateAndAuthUser();

                bool isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.True(isLoggedIn);

                webCookie.Delete(AuthConstants.SessionCookieName);
                webCookie.Delete(AuthConstants.RememberMeCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await this.currentUser.IsLoggedIn();
                Assert.False(isLoggedIn);
            }
        }

        public async Task<int> CreateAndAuthUser()
        {
            string email = Guid.NewGuid().ToString() + "@test.com";

            // create user
            int userId = await authBL.CreateUser(
                new ResumeHub.DAL.Models.UserModel()
                {
                    Email = email,
                    Password = "qwer1234"
                });
            return await authBL.Authenticate(email, "qwer1234", true);
        }
    }
}

