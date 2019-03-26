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
    public partial class CarouselTemplateTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsNull()
            {
                ITemplate template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsInvalid()
            {
                ITemplate template = new CarouselTemplate()
                {
                    Columns = new[] { new CarouselColumn() }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsContainsNull()
            {
                ITemplate template = new CarouselTemplate()
                {
                    Columns = new CarouselColumn[] { null }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The column should not be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ITemplate template = new CarouselTemplate()
                {
                    Columns = new[]
                    {
                        new CarouselColumn()
                        {
                            Text = "Foo",
                            Actions = new[] { new MessageAction() { Label = "Foo", Text = "Bar" } }
                        }
                    }
                };

                template.Validate();
            }
        }
    }
}
