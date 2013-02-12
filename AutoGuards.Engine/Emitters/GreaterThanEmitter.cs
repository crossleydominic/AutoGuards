using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class GreaterThanEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (GreaterThanAttribute); }
        }

        public override StatementSyntax EmitGuard(AttributeData attribute, TypeSymbol parameterType, string parameterName)
        {
            string comparisonValue = attribute.ConstructorArguments.First().Value.ToString();

            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.BinaryExpression(
                SyntaxKind.LessThanOrEqualExpression,
                Syntax.InvocationExpression(
               Syntax.MemberAccessExpression(
                  SyntaxKind.MemberAccessExpression,
                  Syntax.ParenthesizedExpression(
                       Syntax.CastExpression(
                           Syntax.IdentifierName("global::System.IComparable"),
                           Syntax.IdentifierName(parameterName))),
                  name: Syntax.IdentifierName("CompareTo"),
                  operatorToken: Syntax.Token(SyntaxKind.DotToken)),
                  Syntax.ArgumentList(
                      Syntax.SeparatedList(
                          Syntax.Argument(
                              Syntax.LiteralExpression(
                              SyntaxKind.StringLiteralExpression,
                              Syntax.Literal(comparisonValue, "comparisonValue")))))),
                Syntax.IdentifierName("0")),
                Syntax.Block(
                    SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} is not greater than '" + comparisonValue + @"'.""", parameterName))));

            return guardStatement;
        }
    }
}
