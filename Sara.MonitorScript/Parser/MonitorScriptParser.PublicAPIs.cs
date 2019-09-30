using System.Collections.Generic;
using Sara.MonitorScript.Syntax;
using Sara.MonitorScript.Syntax.Expressions;

namespace Sara.MonitorScript.Parser
{
    public sealed partial class MonitorScriptParser
    {
        public Expression ParseExpression(SourceCode sourceCode, IEnumerable<Token> tokens)
        {
            InitializeParser(sourceCode, tokens, MonitorScriptParserOptions.OptionalSemicolons);
            try
            {
                return ParseExpression();
            }
            catch (SyntaxException)
            {
                // Errors are located in the ErrorSink.
                return null;
            }
        }

        public SourceDocument ParseFile(SourceCode sourceCode, IEnumerable<Token> tokens)
        {
            return ParseFile(sourceCode, tokens, MonitorScriptParserOptions.Default);
        }

        public SourceDocument ParseFile(SourceCode sourceCode, IEnumerable<Token> tokens, MonitorScriptParserOptions options)
        {
            InitializeParser(sourceCode, tokens, options);
            try
            {
                return ParseDocument();
            }
            catch (SyntaxException)
            {
                return null;
            }
        }

        public SyntaxNode ParseStatement(SourceCode sourceCode, IEnumerable<Token> tokens)
        {
            return ParseStatement(sourceCode, tokens, MonitorScriptParserOptions.Default);
        }

        public SyntaxNode ParseStatement(SourceCode sourceCode, IEnumerable<Token> tokens, MonitorScriptParserOptions options)
        {
            InitializeParser(sourceCode, tokens, options);

            try
            {
                return ParseStatement();
            }
            catch (SyntaxException)
            {
                return null;
            }
        }
    }
}
