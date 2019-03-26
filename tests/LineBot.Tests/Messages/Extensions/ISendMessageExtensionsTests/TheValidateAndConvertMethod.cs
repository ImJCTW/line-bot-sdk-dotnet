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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ISendMessageExtensionsTests
    {
        [TestClass]
        public class TheValidateAndConvertMethod : ISendMessageExtensionsTests
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCalledWithMoreThanFiveMessages()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[6]);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrarHasNullValue()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { null });
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMessageTypeIsInvalid()
            {
                ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { new InvalidMessage() });
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMessageIsInvalid()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { new TextMessage() });
                });
            }
        }
    }
}
