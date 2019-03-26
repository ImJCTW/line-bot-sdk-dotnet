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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    public partial class ImagemapAreaTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var area = new ImagemapArea()
                {
                    X = 10,
                    Y = 20,
                    Width = 30,
                    Height = 40
                };

                var serialized = JsonConvert.SerializeObject(area);
                Assert.AreEqual(@"{""x"":10,""y"":20,""width"":30,""height"":40}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapArea(1, 2, 3, 4);

                Assert.AreEqual(1, action.X);
                Assert.AreEqual(2, action.Y);
                Assert.AreEqual(3, action.Width);
                Assert.AreEqual(4, action.Height);
            }
        }
    }
}
