using System.Collections.Generic;

namespace Sara.MonitorScript.Syntax.Statements
{
    public class BlockStatement : Statement
    {
        public IEnumerable<SyntaxNode> Contents { get; }

        public override SyntaxKind Kind => SyntaxKind.BlockStatement;

        public BlockStatement(SourceSpan span, IEnumerable<SyntaxNode> contents) : base(span)
        {
            Contents = contents;
        }
    }
}
