using Sara.MonitorScript.Syntax.Expressions;

namespace Sara.MonitorScript.Syntax.Statements
{
    public class WhileStatement : Statement
    {
        public BlockStatement Body { get; }

        public bool IsDoWhile { get; }

        public override SyntaxKind Kind => SyntaxKind.WhileStatement;

        public Expression Predicate { get; }

        public WhileStatement(SourceSpan span, bool isDoWhile, Expression predicate, BlockStatement body) : base(span)
        {
            Body = body;
            Predicate = predicate;
            IsDoWhile = isDoWhile;
        }
    }
}
