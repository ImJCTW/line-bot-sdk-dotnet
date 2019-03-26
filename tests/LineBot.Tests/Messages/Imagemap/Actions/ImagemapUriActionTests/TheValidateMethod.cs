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
    public partial class ImagemapUriActionTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenUrlIsNull()
            {
                var action = new ImagemapUriAction()
                {
                    Area = new ImagemapArea(1, 2, 3, 4)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAreaIsNull()
            {
                var action = new ImagemapUriAction()
                {
                    Url = new Uri("http://foo.bar")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                var action = new ImagemapUriAction()
                {
                    Area = new ImagemapArea(1, 2, 3, 4),
                    Url = new Uri("http://foo.bar")
                };

                action.Validate();
            }
        }
    }
}
