using System.Collections.Generic;

namespace Sara.NETStandard.MonitorScript.Syntax.Expressions
{
    public class MethodCallExpression : Expression
    {
        public IEnumerable<Expression> Arguments { get; }

        public override SyntaxKind Kind => SyntaxKind.MethodCallExpression;

        public Expression Reference { get; }

        public MethodCallExpression(SourceSpan span, Expression reference, IEnumerable<Expression> arguments)
            : base(span)
        {
            Reference = reference;
            Arguments = arguments;
        }
    }
}
