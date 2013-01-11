using System.Collections;
using System.Collections.Generic;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;
using System.Linq;

namespace AutoGuards.Engine.Emitters
{
    public class NotEmptyEmitter : AutoGuardEmitter
    {
        public override System.Type EmitsFor
        {
            get { return typeof (NotEmptyAttribute); }
        }

        public override StatementSyntax EmitGuard(AttributeData attribute, TypeSymbol parameterType, string parameterName)
        {
            //TODO: Modify to try cast to IList first, then try cast to IEnumerable
            //TODO: Consider how to properly handle type conversions
            //TODO: Refactor common expression building into builder
            //TODO: Replace hardcoded names/symbols
            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.BinaryExpression(
                    SyntaxKind.LessThanOrEqualExpression, 
                    Syntax.MemberAccessExpression(
                        SyntaxKind.MemberAccessExpression,
                        Syntax.ParenthesizedExpression(
                            Syntax.CastExpression(
                                Syntax.IdentifierName("global::System.Collections.IList"),
                            Syntax.IdentifierName(parameterName))),
                    name: Syntax.IdentifierName("Count"),
                    operatorToken: Syntax.Token(SyntaxKind.DotToken)),
                            Syntax.IdentifierName("0")),
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
                                                Syntax.Literal(string.Format(@"""{0} cannot be empty""", parameterName), "errorMessage"))),
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