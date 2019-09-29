using System.Collections.Generic;

namespace Sara.NETStandard.MonitorScript.Syntax
{
    public class SourceDocument : SyntaxNode
    {
        public override SyntaxCatagory Catagory => SyntaxCatagory.Document;

        public IEnumerable<SyntaxNode> Children { get; }

        public override SyntaxKind Kind => SyntaxKind.SourceDocument;

        public SourceCode SourceCode { get; }

        public SourceDocument(SourceSpan span, SourceCode sourceCode, IEnumerable<SyntaxNode> children)
            : base(span)
        {
            SourceCode = sourceCode;
            Children = children;
        }
    }
}
