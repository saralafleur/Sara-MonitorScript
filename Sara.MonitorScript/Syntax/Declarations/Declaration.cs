namespace Sara.MonitorScript.Syntax.Declarations
{
    public abstract class Declaration : SyntaxNode
    {
        public override SyntaxCatagory Catagory => SyntaxCatagory.Declaration;

        public string Name { get; }

        protected Declaration(SourceSpan span, string name) : base(span)
        {
            Name = name;
        }
    }
}
