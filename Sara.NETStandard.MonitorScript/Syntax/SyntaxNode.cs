namespace Sara.NETStandard.MonitorScript.Syntax
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxCatagory Catagory { get; }

        public abstract SyntaxKind Kind { get; }

        public SourceSpan Span { get; }

        protected SyntaxNode(SourceSpan span)
        {
            Span = span;
        }
    }
}
