namespace Sara.NETStandard.MonitorScript.Syntax.Statements
{
    public class ContinueStatement : EmptyStatement
    {
        public override SyntaxKind Kind => SyntaxKind.ContinueStatement;

        public ContinueStatement(SourceSpan span) : base(span)
        {
        }
    }
}
