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
    public partial class ConfirmTemplateTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    OkAction = new MessageAction(),
                    CancelAction = new UriAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenOkActionIsNull()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    CancelAction = new UriAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The ok action cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenOkActionIsInvalid()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new UriAction() { Label = "Foo" },
                    CancelAction = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCancelActionIsNull()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The cancel action cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCancelActionIsInvalid()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction() { Label = "Foo", Text = "Bar" },
                    CancelAction = new UriAction() { Label = "Foo" }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction() { Label = "Foo", Text = "Bar" },
                    CancelAction = new MessageAction() { Label = "Foo", Text = "Bar" }
                };

                template.Validate();
            }
        }
    }
}
