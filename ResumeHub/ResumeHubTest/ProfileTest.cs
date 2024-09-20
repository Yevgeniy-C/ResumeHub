using System;
using Resutest.Helpers;
using System.Transactions;

namespace Resutest
{
	public class ProfileTest: Helpers.BaseTest
    {
        [Test]
        public async Task AddTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                await profile.AddOrUpdate(
                    new ResumeHub.DAL.Models.ProfileModel() {
                        UserId = 19,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        ProfileName = "Тест"
                    });

                var results = await profile.Get(19);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("Иван"));
                Assert.That(result.LastName, Is.EqualTo("Иванов"));
                Assert.That(result.ProfileName, Is.EqualTo("Тест"));
                Assert.That(result.UserId, Is.EqualTo(19));
            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionScope())
            {
                var profileModel = new ResumeHub.DAL.Models.ProfileModel()
                {
                    UserId = 19,
                    FirstName = "John",
                    LastName = "Doe",
                    ProfileName = "Test"
                };

                await profile.AddOrUpdate(profileModel);

                profileModel.FirstName = "John1";

                await profile.AddOrUpdate(profileModel);

                var results = await profile.Get(19);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("John1"));
                Assert.That(result.UserId, Is.EqualTo(19));
            }
        }
    }
}

