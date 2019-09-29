﻿using Sara.NETStandard.MonitorScript.Syntax.Expressions;

namespace Sara.NETStandard.MonitorScript.Syntax.Statements
{
    public class ReturnStatement : Statement
    {
        public override SyntaxKind Kind => SyntaxKind.ReturnStatement;

        public Expression Value { get; }

        public ReturnStatement(SourceSpan span, Expression value) : base(span)
        {
            Value = value;
        }
    }
}
