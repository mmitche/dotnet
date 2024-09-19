﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.Razor.ProjectSystem;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.LanguageServer.Protocol;
using Range = Microsoft.VisualStudio.LanguageServer.Protocol.Range;

namespace Microsoft.AspNetCore.Razor.LanguageServer.Diagnostics;

internal static class RazorDiagnosticConverter
{
    public static VSDiagnostic Convert(RazorDiagnostic razorDiagnostic, SourceText sourceText, IDocumentSnapshot? documentSnapshot)
    {
        var diagnostic = new VSDiagnostic()
        {
            Message = razorDiagnostic.GetMessage(CultureInfo.InvariantCulture),
            Code = razorDiagnostic.Id,
            Source = "Razor",
            Severity = ConvertSeverity(razorDiagnostic.Severity),
            // This is annotated as not null, but we have tests that validate the behaviour when
            // we pass in null here
            Range = ConvertSpanToRange(razorDiagnostic.Span, sourceText)!,
            Projects = GetProjectInformation(documentSnapshot)
        };

        return diagnostic;
    }

    public static VSDiagnosticProjectInformation[] GetProjectInformation(IDocumentSnapshot? documentSnapshot)
    {
        if (documentSnapshot is null)
        {
            return [];
        }

        return [new VSDiagnosticProjectInformation()
                {
                    Context = null,
                    ProjectIdentifier = documentSnapshot.Project.Key.Id,
                    ProjectName = documentSnapshot.Project.DisplayName
                }];
    }

    internal static Diagnostic[] Convert(IReadOnlyList<RazorDiagnostic> diagnostics, SourceText sourceText, IDocumentSnapshot documentSnapshot)
    {
        var convertedDiagnostics = new Diagnostic[diagnostics.Count];

        var i = 0;
        foreach (var diagnostic in diagnostics)
        {
            convertedDiagnostics[i++] = Convert(diagnostic, sourceText, documentSnapshot);
        }

        return convertedDiagnostics;
    }

    // Internal for testing
    internal static DiagnosticSeverity ConvertSeverity(RazorDiagnosticSeverity severity)
    {
        return severity switch
        {
            RazorDiagnosticSeverity.Error => DiagnosticSeverity.Error,
            RazorDiagnosticSeverity.Warning => DiagnosticSeverity.Warning,
            _ => DiagnosticSeverity.Information,
        };
    }

    // Internal for testing
    internal static Range? ConvertSpanToRange(SourceSpan sourceSpan, SourceText sourceText)
    {
        if (sourceSpan == SourceSpan.Undefined)
        {
            return null;
        }

        var spanStartIndex = Math.Min(sourceSpan.AbsoluteIndex, sourceText.Length);
        var spanEndIndex = Math.Min(sourceSpan.AbsoluteIndex + sourceSpan.Length, sourceText.Length);

        return sourceText.GetRange(spanStartIndex, spanEndIndex);
    }
}
