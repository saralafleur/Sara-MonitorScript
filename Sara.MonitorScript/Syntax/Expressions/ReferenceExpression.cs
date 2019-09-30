using System.Collections.Generic;

namespace Sara.MonitorScript.Syntax.Expressions
{
    public class ReferenceExpression : Expression
    {
        public override SyntaxKind Kind => SyntaxKind.ReferenceExpression;

        public IEnumerable<Expression> References { get; }

        public ReferenceExpression(SourceSpan span, IEnumerable<Expression> references) : base(span)
        {
            References = references;
        }
    }
}
