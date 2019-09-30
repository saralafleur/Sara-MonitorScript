using Sara.MonitorScript.Syntax.Expressions;

namespace Sara.MonitorScript.Syntax.Declarations
{
    public class FieldDeclaration : Declaration
    {
        public Expression DefaultValue { get; }

        public override SyntaxKind Kind => SyntaxKind.FieldDeclaration;

        public string Type { get; }

        public FieldDeclaration(SourceSpan span, string name, string type, Expression value) : base(span, name)
        {
            Type = type;
            DefaultValue = value;
        }
    }
}
