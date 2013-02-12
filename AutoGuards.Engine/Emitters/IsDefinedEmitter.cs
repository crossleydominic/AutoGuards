﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class IsDefinedEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (IsDefinedAttribute); }
        }

        public override StatementSyntax EmitGuard(AttributeData attribute, TypeSymbol parameterType, string parameterName)
        {            
            //TODO: Consider how to properly handle type conversions
            //TODO: Refactor common expression building into builder
            //TODO: Replace hardcoded names/symbols
            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    Syntax.InvocationExpression(
                        Syntax.MemberAccessExpression(
                            SyntaxKind.MemberAccessExpression,
                            Syntax.IdentifierName("global::System.Enum"),
                            name: Syntax.IdentifierName("IsDefined"),
                            operatorToken: Syntax.Token(SyntaxKind.DotToken)),
                        Syntax.ArgumentList(
                            Syntax.SeparatedList<ArgumentSyntax>(
                                Syntax.Argument(
                                    Syntax.TypeOfExpression(
                                        Syntax.IdentifierName(parameterType.Name))),
                                Syntax.Token(SyntaxKind.CommaToken),
                                Syntax.Argument(
                                    Syntax.LiteralExpression(
                                    SyntaxKind.StringLiteralExpression,
                                    Syntax.Literal(parameterName, parameterName)))))),
                    Syntax.IdentifierName("false")),
                    Syntax.Block(
                        Syntax.ThrowStatement(
                            Syntax.ObjectCreationExpression(
                                Syntax.Token(Syntax.Whitespace(" "), SyntaxKind.NewKeyword, Syntax.Whitespace(" ")),
                                Syntax.QualifiedName(Syntax.IdentifierName("global::System"), Syntax.IdentifierName("ArgumentException")), //TODO: Is there a better way of doing this?
                                Syntax.ArgumentList(
                                    Syntax.SeparatedList<ArgumentSyntax>(
                                        Syntax.Argument(
                                            Syntax.LiteralExpression(
                                                SyntaxKind.StringLiteralExpression,
                                                Syntax.Literal(string.Format(@"""{0} value is not defined for the enum type.""", parameterName), "errorMessage"))),
                                        Syntax.Token(SyntaxKind.CommaToken),
                                        Syntax.Argument(
                                            Syntax.LiteralExpression(
                                                SyntaxKind.StringLiteralExpression,
                                                Syntax.Literal(string.Format(@"""{0}""", parameterName), parameterName))))),
                                Syntax.InitializerExpression(SyntaxKind.ObjectInitializerExpression, new SeparatedSyntaxList<ExpressionSyntax>())))));
            
            return guardStatement;
        }
    }
}