using System;
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
                        SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} cannot be empty""", parameterName))));

            return guardStatement;
        }
    }
}