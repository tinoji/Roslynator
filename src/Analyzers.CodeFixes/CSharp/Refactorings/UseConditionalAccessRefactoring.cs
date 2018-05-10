﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CSharp;
using Roslynator.CSharp.Analysis;
using Roslynator.CSharp.Syntax;
using Roslynator.Text;

namespace Roslynator.CSharp.Refactorings
{
    internal static class UseConditionalAccessRefactoring
    {
        public static async Task<Document> RefactorAsync(
            Document document,
            BinaryExpressionSyntax binaryExpression,
            CancellationToken cancellationToken)
        {
            SemanticModel semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);

            (ExpressionSyntax left, ExpressionSyntax right) = UseConditionalAccessAnalyzer.GetFixableExpressions(binaryExpression, binaryExpression.Kind(), semanticModel, cancellationToken);

            NullCheckExpressionInfo nullCheck = SyntaxInfo.NullCheckExpressionInfo(left, allowedStyles: NullCheckStyles.NotEqualsToNull);

            ExpressionSyntax expression = nullCheck.Expression;

            bool isNullable = semanticModel.GetTypeSymbol(expression, cancellationToken).IsNullableType();

            ExpressionSyntax expression2 = UseConditionalAccessAnalyzer.FindExpressionThatCanBeConditionallyAccessed(
                expression,
                right,
                isNullable: isNullable);

            var builder = new SyntaxNodeTextBuilder(binaryExpression, StringBuilderCache.GetInstance(binaryExpression.FullSpan.Length));

            builder.Append(TextSpan.FromBounds(binaryExpression.FullSpan.Start, left.Span.Start));

            int parenDiff = GetParenTokenDiff();

            if (parenDiff > 0)
                builder.Append('(', parenDiff);

            builder.AppendSpan(expression);
            builder.Append("?");
            builder.Append(TextSpan.FromBounds(expression2.Span.End, right.Span.End));

            switch (right.Kind())
            {
                case SyntaxKind.LogicalOrExpression:
                case SyntaxKind.LogicalAndExpression:
                case SyntaxKind.BitwiseOrExpression:
                case SyntaxKind.BitwiseAndExpression:
                case SyntaxKind.ExclusiveOrExpression:
                case SyntaxKind.EqualsExpression:
                case SyntaxKind.NotEqualsExpression:
                case SyntaxKind.LessThanExpression:
                case SyntaxKind.LessThanOrEqualExpression:
                case SyntaxKind.GreaterThanExpression:
                case SyntaxKind.GreaterThanOrEqualExpression:
                case SyntaxKind.IsExpression:
                case SyntaxKind.AsExpression:
                case SyntaxKind.IsPatternExpression:
                    {
                        break;
                    }
                case SyntaxKind.LogicalNotExpression:
                    {
                        builder.Append(" == false");
                        break;
                    }
                default:
                    {
                        builder.Append(" == true");
                        break;
                    }
            }

            if (parenDiff < 0)
                builder.Append(')', -parenDiff);

            builder.Append(TextSpan.FromBounds(right.Span.End, binaryExpression.FullSpan.End));

            string text = StringBuilderCache.GetStringAndFree(builder.StringBuilder);

            ParenthesizedExpressionSyntax newNode = SyntaxFactory.ParseExpression(text)
                .WithFormatterAnnotation()
                .Parenthesize();

            return await document.ReplaceNodeAsync(binaryExpression, newNode, cancellationToken).ConfigureAwait(false);

            int GetParenTokenDiff()
            {
                int count = 0;

                foreach (SyntaxToken token in binaryExpression.DescendantTokens(TextSpan.FromBounds(left.Span.Start, expression2.Span.End)))
                {
                    SyntaxKind kind = token.Kind();

                    if (kind == SyntaxKind.OpenParenToken)
                    {
                        if (token.IsParentKind(SyntaxKind.ParenthesizedExpression))
                            count++;
                    }
                    else if (kind == SyntaxKind.CloseParenToken)
                    {
                        if (token.IsParentKind(SyntaxKind.ParenthesizedExpression))
                            count--;
                    }
                }

                return count;
            }
        }

        public static async Task<Document> RefactorAsync(
            Document document,
            IfStatementSyntax ifStatement,
            CancellationToken cancellationToken)
        {
            var statement = (ExpressionStatementSyntax)ifStatement.SingleNonBlockStatementOrDefault();

            StatementSyntax newStatement = statement;

            NullCheckExpressionInfo nullCheck = SyntaxInfo.NullCheckExpressionInfo(ifStatement.Condition, NullCheckStyles.NotEqualsToNull);

            SimpleMemberInvocationStatementInfo invocationInfo = SyntaxInfo.SimpleMemberInvocationStatementInfo(statement);

            ExpressionSyntax expression = invocationInfo.Expression;

            SemanticModel semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);

            if (semanticModel.GetTypeSymbol(nullCheck.Expression, cancellationToken).IsNullableType())
            {
                var memberAccess = (MemberAccessExpressionSyntax)invocationInfo.Expression;

                newStatement = statement.ReplaceNode(memberAccess, memberAccess.Expression.WithTrailingTrivia(memberAccess.GetTrailingTrivia()));

                expression = memberAccess.Expression;
            }

            int insertIndex = expression.Span.End - statement.FullSpan.Start;

            newStatement = SyntaxFactory.ParseStatement(newStatement.ToFullString().Insert(insertIndex, "?"));

            IEnumerable<SyntaxTrivia> leading = ifStatement.DescendantTrivia(TextSpan.FromBounds(ifStatement.SpanStart, statement.SpanStart));

            newStatement = (leading.All(f => f.IsWhitespaceOrEndOfLineTrivia()))
                ? newStatement.WithLeadingTrivia(ifStatement.GetLeadingTrivia())
                : newStatement.WithLeadingTrivia(ifStatement.GetLeadingTrivia().Concat(leading));

            IEnumerable<SyntaxTrivia> trailing = ifStatement.DescendantTrivia(TextSpan.FromBounds(statement.Span.End, ifStatement.Span.End));

            newStatement = (leading.All(f => f.IsWhitespaceOrEndOfLineTrivia()))
                ? newStatement.WithTrailingTrivia(ifStatement.GetTrailingTrivia())
                : newStatement.WithTrailingTrivia(trailing.Concat(ifStatement.GetTrailingTrivia()));

            return await document.ReplaceNodeAsync(ifStatement, newStatement, cancellationToken).ConfigureAwait(false);
        }
    }
}
