using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class MatchesEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (MatchesAttribute); }
        }

        public override StatementSyntax EmitGuard(AttributeData attribute, TypeSymbol parameterType, string parameterName)
        {
            //TODO: Make defensive incase no argument supplied
            string regexMatchString = attribute.ConstructorArguments.First().Value.ToString();
            
            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.BinaryExpression(
                SyntaxKind.EqualsExpression,
                Syntax.InvocationExpression(
               Syntax.MemberAccessExpression(
                   SyntaxKind.MemberAccessExpression,
                   Syntax.IdentifierName("global::System.Text.RegularExpressions.Regex"),
                   name: Syntax.IdentifierName("IsMatch"),
                   operatorToken: Syntax.Token(SyntaxKind.DotToken)),
               Syntax.ArgumentList(
                   Syntax.SeparatedList<ArgumentSyntax>(
                       Syntax.Argument(
                            Syntax.IdentifierName(parameterName)),    
                       Syntax.Token(SyntaxKind.CommaToken),
                       Syntax.Argument(
                           Syntax.LiteralExpression(
                           SyntaxKind.StringLiteralExpression,
                           Syntax.Literal(@"""" + regexMatchString + @"""", "regexMatchString")))))), 
                Syntax.IdentifierName("false")),
                Syntax.Block(
                    SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} does not match the regular expression '" + regexMatchString + @"'.""", parameterName))));

            return guardStatement;
        }
    }
}
