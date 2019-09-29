using Sara.NETStandard.MonitorScript.Syntax.Expressions;

namespace Sara.NETStandard.MonitorScript.Syntax.Declarations
{
    public class VariableDeclaration : Declaration
    {
        public override SyntaxKind Kind => SyntaxKind.VariableDeclaration;

        public string Type { get; }

        public Expression Value { get; }

        public VariableDeclaration(SourceSpan span, string name, string type, Expression value) : base(span, name)
        {
            Type = type;
            Value = value;
        }
    }
}
