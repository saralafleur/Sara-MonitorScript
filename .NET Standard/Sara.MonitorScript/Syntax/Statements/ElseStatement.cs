namespace Sara.MonitorScript.Syntax.Statements
{
    public class ElseStatement : Statement
    {
        public BlockStatement Body { get; }

        public override SyntaxKind Kind => SyntaxKind.ElseStatement;

        public ElseStatement(SourceSpan span, BlockStatement body) : base(span)
        {
            Body = body;
        }
    }
}
