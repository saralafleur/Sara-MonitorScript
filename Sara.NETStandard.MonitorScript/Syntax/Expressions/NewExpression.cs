using System.Collections.Generic;

namespace Sara.NETStandard.MonitorScript.Syntax.Expressions
{
    public class NewExpression : Expression
    {
        public IEnumerable<Expression> Arguments { get; }

        public override SyntaxKind Kind => SyntaxKind.NewExpression;

        public Expression Reference { get; }

        public NewExpression(SourceSpan span, Expression reference, IEnumerable<Expression> arguments) : base(span)
        {
            Reference = reference;
            Arguments = arguments;
        }
    }
}
