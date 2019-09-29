﻿namespace Sara.MonitorScript.Syntax.Declarations
{
    public class ParameterDeclaration : Declaration
    {
        public override SyntaxKind Kind => SyntaxKind.ParameterDeclaration;

        public string Type { get; }

        public ParameterDeclaration(SourceSpan span, string name, string type) : base(span, name)
        {
            Type = type;
        }
    }
}
