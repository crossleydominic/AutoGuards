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
                Syntax.ThrowStatement(
                Syntax.ObjectCreationExpression(
              Syntax.Token(Syntax.Whitespace(" "), SyntaxKind.NewKeyword, Syntax.Whitespace(" ")),
              Syntax.QualifiedName(Syntax.IdentifierName("global::System"), Syntax.IdentifierName("ArgumentException")), //TODO: Is there a better way of doing this?
              Syntax.ArgumentList(
                  Syntax.SeparatedList<ArgumentSyntax>(
                      Syntax.Argument(
                          Syntax.LiteralExpression(
                              SyntaxKind.StringLiteralExpression,
                              Syntax.Literal(string.Format(@"""{0} is not greater than '" + comparisonValue + @"'.""", parameterName), "errorMessage"))),
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
