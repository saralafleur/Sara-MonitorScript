using System.Collections.Generic;

namespace Sara.NETStandard.MonitorScript.Syntax.Declarations
{
    public class OptionDeclaration : Declaration
    {
        public override SyntaxKind Kind => SyntaxKind.OptionDeclaration;

        public string Value { get; }

        public List<string> Parameters { get;  }

        public OptionDeclaration(SourceSpan span, string name, string value, List<string> parameters) : base(span, name)
        {
            Value = value;
            Parameters = parameters;
        }
    }
}
