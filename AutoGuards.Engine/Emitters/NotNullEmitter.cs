﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.API;
using Roslyn.Compilers.CSharp;

namespace AutoGuards.Engine.Emitters
{
    public class NotNullEmitter : AutoGuardEmitter
    {
        public override Type EmitsFor
        {
            get { return typeof (NotNullAttribute); }
        }

        public override StatementSyntax EmitGuard(TypeSymbol parameterType, string parameterName)
        {
            StatementSyntax guardStatement = Syntax.IfStatement(
                Syntax.BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    Syntax.IdentifierName(parameterName),
                    Syntax.IdentifierName("null")),
                    Syntax.Block( 
                        Syntax.ThrowStatement(
                            Syntax.ObjectCreationExpression(
                                Syntax.Token(Syntax.Whitespace(" "), SyntaxKind.NewKeyword, Syntax.Whitespace(" ")),
                                Syntax.QualifiedName(Syntax.IdentifierName("global::System"), Syntax.IdentifierName("ArgumentNullException")), 
                                Syntax.ArgumentList(Syntax.SeparatedList(Syntax.Argument(
                                    Syntax.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        Syntax.Literal(string.Format(@"""{0}""", parameterName), parameterName))))), 
                                Syntax.InitializerExpression(SyntaxKind.ObjectInitializerExpression, new SeparatedSyntaxList<ExpressionSyntax>())))));

            return guardStatement;
        }
    }
}
