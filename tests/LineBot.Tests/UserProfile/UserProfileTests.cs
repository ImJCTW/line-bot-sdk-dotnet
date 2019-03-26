﻿// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class UserProfileTests
    {
        [TestMethod]
        public async Task GetProfile_UserIsNulll_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
            {
                IUserProfile profile = await bot.GetProfile((IUser)null);
            });
        }

        [TestMethod]
        public async Task GetProfile_UserIdIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
            {
                IUserProfile profile = await bot.GetProfile((string)null);
            });
        }

        [TestMethod]
        public async Task GetProfile_UserIdIsEmpty_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
            {
                IUserProfile profile = await bot.GetProfile(string.Empty);
            });
        }

        [TestMethod]
        public async Task GetProfile_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.GetProfile("test");
            });
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.UserProfile)]
        public async Task GetProfile_CorrectResponse_ReturnsUserProfile()
        {
            TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.UserProfile);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            IUserProfile profile = await bot.GetProfile("test");

            Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
            Assert.AreEqual("/profile/test", httpClient.RequestPath);

            Assert.IsNotNull(profile);
            Assert.AreEqual("LINE taro", profile.DisplayName);
            Assert.AreEqual(new Uri("http://obs.line-apps.com/..."), profile.PictureUrl);
            Assert.AreEqual("Hello, LINE!", profile.StatusMessage);
            Assert.AreEqual("Uxxxxxxxxxxxxxx...", profile.UserId);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.UserProfile)]
        public async Task GetProfile_WithUser_ReturnsUserProfile()
        {
            TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.UserProfile);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            IUserProfile profile = await bot.GetProfile(new TestUser());

            Assert.AreEqual("/profile/testUser", httpClient.RequestPath);
            Assert.IsNotNull(profile);
        }
    }
}
