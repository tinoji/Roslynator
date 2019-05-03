﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CSharp;
using Roslynator.CSharp.Syntax;

namespace Roslynator.CodeAnalysis.CSharp
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SimpleMemberAccessExpressionAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(
                    DiagnosticDescriptors.UsePropertySyntaxNodeSpanStart,
                    DiagnosticDescriptors.CallAnyInsteadOfUsingCount);
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterSyntaxNodeAction(AnalyzeSimpleMemberAccessExpression, SyntaxKind.SimpleMemberAccessExpression);
        }

        private static void AnalyzeSimpleMemberAccessExpression(SyntaxNodeAnalysisContext context)
        {
            var memberAccessExpression = (MemberAccessExpressionSyntax)context.Node;

            SimpleNameSyntax name = memberAccessExpression.Name;

            switch (name.Kind())
            {
                case SyntaxKind.IdentifierName:
                    {
                        var identifierName = (IdentifierNameSyntax)name;

                        switch (identifierName.Identifier.ValueText)
                        {
                            case "Start":
                                {
                                    ExpressionSyntax expression = memberAccessExpression.Expression;

                                    if (!expression.IsKind(SyntaxKind.SimpleMemberAccessExpression))
                                        break;

                                    ISymbol symbol = context.SemanticModel.GetSymbol(memberAccessExpression, context.CancellationToken);

                                    if (symbol == null)
                                        break;

                                    if (!symbol.ContainingType.HasMetadataName(RoslynMetadataNames.Microsoft_CodeAnalysis_Text_TextSpan))
                                        break;

                                    var memberAccess2 = (MemberAccessExpressionSyntax)expression;

                                    SimpleNameSyntax name2 = memberAccess2.Name;

                                    if (!name2.IsKind(SyntaxKind.IdentifierName))
                                        break;

                                    var identifierName2 = (IdentifierNameSyntax)name2;

                                    if (!string.Equals(identifierName2.Identifier.ValueText, "Span", StringComparison.Ordinal))
                                        break;

                                    ISymbol symbol2 = context.SemanticModel.GetSymbol(expression, context.CancellationToken);

                                    if (symbol2 == null)
                                        break;

                                    if (!symbol2.ContainingType.HasMetadataName(RoslynMetadataNames.Microsoft_CodeAnalysis_SyntaxNode))
                                        break;

                                    context.ReportDiagnostic(DiagnosticDescriptors.UsePropertySyntaxNodeSpanStart, memberAccessExpression);
                                    break;
                                }
                            case "Count":
                                {
                                    CallAnyInsteadOfUsingCount();
                                    break;
                                }
                        }

                        break;
                    }
            }

            void CallAnyInsteadOfUsingCount()
            {
                SyntaxNode expression = memberAccessExpression.WalkUpParentheses();

                SyntaxNode parent = expression.Parent;

                if (!parent.IsKind(SyntaxKind.EqualsExpression, SyntaxKind.NotEqualsExpression, SyntaxKind.GreaterThanExpression))
                    return;

                BinaryExpressionInfo binaryExpressionInfo = SyntaxInfo.BinaryExpressionInfo((BinaryExpressionSyntax)parent);

                if (!binaryExpressionInfo.Success)
                    return;

                ExpressionSyntax otherExpression = (expression == binaryExpressionInfo.Left)
                    ? binaryExpressionInfo.Right
                    : binaryExpressionInfo.Left;

                if (!otherExpression.IsKind(SyntaxKind.NumericLiteralExpression))
                    return;

                var numericLiteralExpression = (LiteralExpressionSyntax)otherExpression;

                if (numericLiteralExpression.Token.ValueText != "0")
                    return;

                ISymbol symbol = context.SemanticModel.GetSymbol(memberAccessExpression, context.CancellationToken);

                if (symbol?.Kind != SymbolKind.Property
                    || symbol.IsStatic
                    || symbol.DeclaredAccessibility != Accessibility.Public
                    || !RoslynSymbolUtility.IsList(symbol.ContainingType.OriginalDefinition))
                {
                    return;
                }

                TextSpan span = (memberAccessExpression == binaryExpressionInfo.Left)
                    ? TextSpan.FromBounds(name.SpanStart, numericLiteralExpression.Span.End)
                    : TextSpan.FromBounds(numericLiteralExpression.SpanStart, name.Span.End);

                context.ReportDiagnostic(DiagnosticDescriptors.CallAnyInsteadOfUsingCount, Location.Create(memberAccessExpression.SyntaxTree, span));
            }
        }
    }
}
