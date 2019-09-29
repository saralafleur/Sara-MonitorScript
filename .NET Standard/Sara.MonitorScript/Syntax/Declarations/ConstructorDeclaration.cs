using System.Collections.Generic;
using Sara.MonitorScript.Syntax.Statements;

namespace Sara.MonitorScript.Syntax.Declarations
{
    public class ConstructorDeclaration : Declaration
    {
        public BlockStatement Body { get; }

        public override SyntaxKind Kind => SyntaxKind.ConstructorDeclaration;

        public IEnumerable<ParameterDeclaration> Parameters { get; }

        public ConstructorDeclaration(SourceSpan span, string name, IEnumerable<ParameterDeclaration> parameters, BlockStatement body) : base(span, name)
        {
            Body = body;
            Parameters = parameters;
        }
    }
}
