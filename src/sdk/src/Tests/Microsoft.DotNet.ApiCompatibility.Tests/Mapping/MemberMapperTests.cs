﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable enable

using Microsoft.DotNet.ApiCompatibility.Mapping;
using Microsoft.DotNet.ApiCompatibility.Rules;
using Moq;
using Xunit;

namespace Microsoft.DotNet.ApiCompatibility.Tests.Mapping
{
    public class MemberMapperTests
    {
        [Fact]
        public void MemberMapper_Ctor_PropertiesSet()
        {
            IRuleRunner ruleRunner = Mock.Of<IRuleRunner>();
            IMapperSettings mapperSettings = Mock.Of<IMapperSettings>();
            int rightSetSize = 5;
            ITypeMapper containingType = Mock.Of<ITypeMapper>();

            MemberMapper memberMapper = new(ruleRunner, mapperSettings, rightSetSize, containingType);

            Assert.Null(memberMapper.Left);
            Assert.Equal(mapperSettings, memberMapper.Settings);
            Assert.Equal(rightSetSize, memberMapper.Right.Length);
            Assert.Equal(containingType, memberMapper.ContainingType);
        }
    }
}
