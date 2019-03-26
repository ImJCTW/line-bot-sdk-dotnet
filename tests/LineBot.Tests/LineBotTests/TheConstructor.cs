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
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ThrowsExceptionWhenConfigurationIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
                {
                    new LineBot(null);
                });
            }

            [TestMethod]
            public void ThrowsNotExceptionWhenLoggerIsNull()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = "ChannelSecret",
                };

                new LineBot(configuration, null);
            }

            [TestMethod]
            public void ThrowsExceptionWhenChannelAccessTokenIsNull()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = null,
                    ChannelSecret = "ChannelSecret",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelAccessToken cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [TestMethod]
            public void ThrowsExceptionWhenChannelAccessTokenIsEmpty()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = string.Empty,
                    ChannelSecret = "ChannelSecret",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelAccessToken cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [TestMethod]
            public void ThrowsExceptionWhenChannelAccessTokenIsWhitespace()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "  ",
                    ChannelSecret = "ChannelSecret",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelAccessToken cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [TestMethod]
            public void ThrowsExceptionWhenChannelSecretTokenIsNull()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = null,
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelSecret cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [TestMethod]
            public void ThrowsExceptionWhenChannelSecretTokenIsEmpty()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = string.Empty,
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelSecret cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [TestMethod]
            public void ThrowsExceptionWhenChannelSecretTokenIsWhitespace()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = "  ",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelSecret cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [TestMethod]
            public void ShouldSetBaseAddressToApiWhenHttpFactoryIsUsed()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = "ChannelSecret",
                };

                ILineBot bot = new LineBot(configuration);

                var field = bot.GetType().GetRuntimeFields().Where(f => f.Name == "_client").First();
                var client = (HttpClient)field.GetValue(bot);

                Assert.AreEqual(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
            }
        }
    }
}
