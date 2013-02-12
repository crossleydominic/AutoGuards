using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using AutoGuards.Engine.Expressions;
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
                SimpleSyntaxWriter.InvokeMethodWithCast(
                    (IComparable x)=> x.CompareTo(Fake.Object), 
                    parameterName,
                    SimpleSyntaxWriter.ArgumentFromLiteral(comparisonValue, false)
                ),
                Syntax.IdentifierName("0")),
                Syntax.Block(
                    SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} is not greater than '" + comparisonValue + @"'.""", parameterName))));

            return guardStatement;
        }
    }
}
