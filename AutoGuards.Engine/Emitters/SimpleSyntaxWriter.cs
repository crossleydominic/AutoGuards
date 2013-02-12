using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.Engine.Expressions;
using AutoGuards.Engine.Extensions;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public static class SimpleSyntaxWriter
    {
        public static InvocationExpressionSyntax InvokeMethodWithCast<TIn, TOut>(Expression<Func<TIn, TOut>> exp, string parameterName, params ArgumentSyntax[] arguments)
        {
            return Syntax.InvocationExpression(
                        AccessMemberWithCast(exp, parameterName),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList<ArgumentSyntax>(arguments, Enumerable.Repeat(Syntax.Token(SyntaxKind.CommaToken), arguments.Length - 1))));
        }

        public static InvocationExpressionSyntax InvokeStaticMethod<T>(Expression<Func<T>> exp, params ArgumentSyntax[] arguments)
        {
            return Syntax.InvocationExpression(
                        AccessStaticMember(exp),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList<ArgumentSyntax>(arguments, Enumerable.Repeat(Syntax.Token(SyntaxKind.CommaToken), arguments.Length - 1))));
        }

        public static ArgumentSyntax ArgumentFromTypeof(string typeName)
        {
            return Syntax.Argument(Syntax.TypeOfExpression(Syntax.IdentifierName(typeName)));
        }

        public static ArgumentSyntax ArgumentFromIdentifier(string identifier)
        {
            return Syntax.Argument(Syntax.IdentifierName(identifier));
        }

        public static ArgumentSyntax ArgumentFromLiteral(string literal, bool requiresQuotes)
        {
            string literalValue = requiresQuotes ? @"""" + literal + @"""" : literal;

            return Syntax.Argument(
                Syntax.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Syntax.Literal(literalValue, "literalValue")));
        }

        public static MemberAccessExpressionSyntax AccessStaticMember<T>(Expression<Func<T>> exp)
        {
            Invocation mi = ExpressionInspector.GetInvocation(exp.Body);

            return Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.IdentifierName(mi.DeclaringType.GetGlobalName()),
                        name: Syntax.IdentifierName(mi.MethodName),
                        operatorToken: Syntax.Token(SyntaxKind.DotToken));
        }

        public static MemberAccessExpressionSyntax AccessMemberWithCast<TIn, TOut>(Expression<Func<TIn, TOut>> exp, string parameterName)
        {
            Invocation mi = ExpressionInspector.GetInvocation(exp.Body);

            return Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.ParenthesizedExpression(
                            Syntax.CastExpression(
                                Syntax.IdentifierName(mi.DeclaringType.GetGlobalName()),
                                Syntax.IdentifierName(parameterName))),
                        name: Syntax.IdentifierName(mi.MethodName),
                        operatorToken: Syntax.Token(SyntaxKind.DotToken));
        }

        public static ThrowStatementSyntax GenerateThrowStatement(Type exceptionType, string parameterName)
        {
            return GenerateThrowStatement(exceptionType, parameterName, null);
        }

        public static ThrowStatementSyntax GenerateThrowStatement(Type exceptionType, string parameterName, string errorMessage)
        {
            ArgumentListSyntax argumentList;
            ArgumentSyntax parameterNameArgument, errorMessageArgument;

            parameterNameArgument = errorMessageArgument = null;

            parameterNameArgument = Syntax.Argument(
                Syntax.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Syntax.Literal(string.Format(@"""{0}""", parameterName), parameterName)));

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                argumentList = Syntax.ArgumentList(
                    Syntax.SeparatedList<ArgumentSyntax>(parameterNameArgument));
            }
            else
            {
                errorMessageArgument = Syntax.Argument(
                    Syntax.LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Syntax.Literal(errorMessage, "errorMessage")));

                argumentList = Syntax.ArgumentList(
                    Syntax.SeparatedList<ArgumentSyntax>(
                        parameterNameArgument,
                        Syntax.Token(SyntaxKind.CommaToken),
                        errorMessageArgument));
            }

            return Syntax.ThrowStatement(
                Syntax.ObjectCreationExpression(
                    Syntax.Token(Syntax.Whitespace(" "), SyntaxKind.NewKeyword, Syntax.Whitespace(" ")),
                    Syntax.IdentifierName(exceptionType.GetGlobalName()), 
                    argumentList,
                    Syntax.InitializerExpression(SyntaxKind.ObjectInitializerExpression, new SeparatedSyntaxList<ExpressionSyntax>())));
        }
    }
}
