using System.Collections.Generic;
using Sara.NETStandard.MonitorScript.Syntax.Statements;

namespace Sara.NETStandard.MonitorScript.Syntax.Declarations
{
    public class PatternDeclaration : Declaration
    {
        public BlockStatement Body { get; }

        public override SyntaxKind Kind => SyntaxKind.PatternDeclaration;

        public IEnumerable<OptionDeclaration> Options { get; }
        public IEnumerable<SourceDeclaration> Sources { get; }

        public PatternDeclaration(SourceSpan span, string name, IEnumerable<OptionDeclaration> options, IEnumerable<SourceDeclaration> sources, BlockStatement body) : base(span, name)
        {
            Options = options;
            Sources = sources;
            Body = body;
        }
    }
}
