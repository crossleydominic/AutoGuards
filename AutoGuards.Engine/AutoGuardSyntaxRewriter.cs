using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoGuards.Engine.Emitters;
using Roslyn.Compilers.CSharp;
using Roslyn.Compilers.Common;

namespace AutoGuards.Engine
{
    public class AutoGuardSyntaxRewriter : SyntaxRewriter
    {
        private MethodInspector _inspector;
        private CommonCompilation _compilation;
        private SemanticModel _semanticModel;

        public AutoGuardSyntaxRewriter(CommonCompilation compilation, SemanticModel semanticModel)
        {
            _inspector = new MethodInspector();
            _compilation = compilation;
            _semanticModel = semanticModel;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var methodDecl = (MethodDeclarationSyntax)base.VisitMethodDeclaration(node);
            
            List<GuardedParameter> guardsToEmit = _inspector.Inspect(_compilation, _semanticModel, methodDecl);

            List<StatementSyntax> bodyStatements = guardsToEmit.SelectMany(g => g.Emitters.Select(y => y.Emitter.EmitGuard(y.Attribute, g.ParameterType, g.ParameterName))).ToList();

            //Add original body back in
            bodyStatements.AddRange(methodDecl.Body.Statements);

            return Syntax.MethodDeclaration(
                methodDecl.AttributeLists,
                methodDecl.Modifiers,
                methodDecl.ReturnType,
                methodDecl.ExplicitInterfaceSpecifier,
                methodDecl.Identifier,
                methodDecl.TypeParameterList,
                methodDecl.ParameterList,
                methodDecl.ConstraintClauses,
                Syntax.Block(bodyStatements));
        }
    }
}
