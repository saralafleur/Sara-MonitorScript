using System.Collections.Generic;
using Sara.MonitorScript.Syntax.Expressions;

namespace Sara.MonitorScript.Syntax.Statements
{
    public class CaseStatement : Statement
    {
        public IEnumerable<SyntaxNode> Body { get; }

        public IEnumerable<Expression> Cases { get; }

        public override SyntaxKind Kind => SyntaxKind.CaseStatement;

        public CaseStatement(SourceSpan span, IEnumerable<Expression> cases, IEnumerable<SyntaxNode> body) : base(span)
        {
            Body = body;
            Cases = cases;
        }
    }
}
