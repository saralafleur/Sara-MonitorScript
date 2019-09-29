namespace Sara.NETStandard.MonitorScript.Syntax.Expressions
{
    public abstract class Expression : SyntaxNode
    {
        public override SyntaxCatagory Catagory => SyntaxCatagory.Expression;

        protected Expression(SourceSpan span) : base(span)
        {
        }
    }
}
