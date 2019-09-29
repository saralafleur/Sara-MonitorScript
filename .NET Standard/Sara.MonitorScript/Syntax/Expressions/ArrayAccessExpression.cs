using System.Collections.Generic;

namespace Sara.MonitorScript.Syntax.Expressions
{
    public class ArrayAccessExpression : Expression
    {
        public IEnumerable<Expression> Arguments { get; }

        public override SyntaxKind Kind => SyntaxKind.ArrayAccessExpression;

        public Expression Reference { get; }

        public ArrayAccessExpression(SourceSpan span, Expression reference, IEnumerable<Expression> arguments) : base(span)
        {
            Reference = reference;
            Arguments = arguments;
        }
    }
}
