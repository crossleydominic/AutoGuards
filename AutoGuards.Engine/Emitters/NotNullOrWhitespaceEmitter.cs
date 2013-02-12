using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class NotNullOrWhitespaceEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (NotNullOrWhitespaceAttribute); }
        }

        public override StatementSyntax EmitGuard(AttributeData attribute, TypeSymbol parameterType, string parameterName)
        {
            //TODO: Consider how to properly handle type conversions
            //TODO: Refactor common expression building into builder
            //TODO: Replace hardcoded names/symbols
            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.InvocationExpression(
                    Syntax.MemberAccessExpression(
                    SyntaxKind.MemberAccessExpression,
                    Syntax.IdentifierName("global::System.String"),
                    name: Syntax.IdentifierName("IsNullOrWhiteSpace"),
                    operatorToken: Syntax.Token(SyntaxKind.DotToken)),
                            Syntax.ArgumentList(
                                Syntax.SeparatedList(
                                    Syntax.Argument(
                                        Syntax.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Syntax.Literal(parameterName, parameterName)))))),
                    Syntax.Block(
                        SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} cannot be null, empty or whitespace""", parameterName))));

            return guardStatement;
        }
    }
}
