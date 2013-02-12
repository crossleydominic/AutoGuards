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
            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.BinaryExpression(
                    SyntaxKind.LessThanOrEqualExpression, 
                    SimpleSyntaxWriter.AccessMemberWithCast((IList x)=>x.Count, parameterName),
                            Syntax.IdentifierName("0")),
                    Syntax.Block(
                        SimpleSyntaxWriter.GenerateThrowStatement(typeof(ArgumentException), parameterName, string.Format(@"""{0} cannot be empty""", parameterName))));

            return guardStatement;
        }
    }
}