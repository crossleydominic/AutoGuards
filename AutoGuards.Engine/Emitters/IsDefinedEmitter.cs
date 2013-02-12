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
    public class IsDefinedEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (IsDefinedAttribute); }
        }

        public override StatementSyntax EmitGuard(AttributeData attribute, TypeSymbol parameterType, string parameterName)
        {            
            //TODO: Consider how to properly handle type conversions
            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    SimpleSyntaxWriter.InvokeStaticMethod(
                        () => Enum.IsDefined(Fake.Any<Type>(), Fake.Object),
                        SimpleSyntaxWriter.ArgumentFromTypeof(parameterType.Name),
                        SimpleSyntaxWriter.ArgumentFromLiteral(parameterName, false)
                    ),
                    Syntax.IdentifierName("false")),
                    Syntax.Block(
                        SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} value is not defined for the enum type.""", parameterName))));
            
            return guardStatement;
        }
    }
}
