using System.Collections.Generic;
using Sara.NETStandard.MonitorScript.Syntax.Declarations;
using Sara.NETStandard.MonitorScript.Syntax.Statements;

namespace Sara.NETStandard.MonitorScript.Syntax.Expressions
{
    public class LambdaExpression : Expression
    {
        public BlockStatement Body { get; }

        public override SyntaxKind Kind => SyntaxKind.LambdaExpression;

        public IEnumerable<ParameterDeclaration> Parameters { get; }

        public LambdaExpression(SourceSpan span, IEnumerable<ParameterDeclaration> parameters, BlockStatement body) : base(span)
        {
            Parameters = parameters;
            Body = body;
        }

        public MethodDeclaration ToMethodDeclaration(string name)
        {
            return new MethodDeclaration(Span, name, "Object", Parameters, Body);
        }
    }
}
