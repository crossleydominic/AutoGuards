using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoGuards.API;
using AutoGuards.Engine.Expressions;
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
                    SimpleSyntaxWriter.InvokeStaticMethod(
                    () => Regex.IsMatch(It.Is<string>(), It.Is<string>()),
                        SimpleSyntaxWriter.ArgumentFromIdentifier(parameterName), 
                        SimpleSyntaxWriter.ArgumentFromLiteral(regexMatchString, true)
                    ), 
                    Syntax.IdentifierName("false")),
                Syntax.Block(
                    SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} does not match the regular expression '" + regexMatchString + @"'.""", parameterName))));

            return guardStatement;
        }
    }
}
