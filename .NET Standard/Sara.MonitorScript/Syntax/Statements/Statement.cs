namespace Sara.MonitorScript.Syntax.Statements
{
    public abstract class Statement : SyntaxNode
    {
        public override SyntaxCatagory Catagory => SyntaxCatagory.Statement;

        protected Statement(SourceSpan span) : base(span)
        {
        }
    }
}
