// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Microsoft.AspNetCore.Http;

internal static class JsonSerializerExtensions
{
    public static bool IsPolymorphicSafe(this JsonTypeInfo jsonTypeInfo)
     => jsonTypeInfo.Type.IsSealed || jsonTypeInfo.Type.IsValueType || jsonTypeInfo.PolymorphismOptions is not null;

    public static JsonTypeInfo GetReadOnlyTypeInfo(this JsonSerializerOptions options, Type type)
    {
        options.MakeReadOnly();
        return options.GetTypeInfo(type);
    }
}
