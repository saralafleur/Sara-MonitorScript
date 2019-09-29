namespace Sara.NETStandard.MonitorScript.Syntax.Declarations
{
    public class SourceDeclaration : Declaration
    {
        public override SyntaxKind Kind => SyntaxKind.SourceDeclaration;

        public string Value { get; }

        public SourceDeclaration(SourceSpan span, string name, string value) : base(span, name)
        {
            Value = value;
        }
    }
}
