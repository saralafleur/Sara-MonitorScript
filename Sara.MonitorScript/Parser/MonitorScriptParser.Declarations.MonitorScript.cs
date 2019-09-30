using System.Collections.Generic;
using System.Linq;
using Sara.Common.Extension;
using Sara.MonitorScript.Lexer;
using Sara.MonitorScript.Syntax;
using Sara.MonitorScript.Syntax.Declarations;
using Sara.MonitorScript.Syntax.Statements;

namespace Sara.MonitorScript.Parser
{
    public sealed partial class MonitorScriptParser
    {
        private SourceDocument ParseDocument()
        {
            List<SyntaxNode> contents = new List<SyntaxNode>();

            var start = _current.Span.Start;
            while (_current == Keyword._class || _current == Keyword._func || _current == Keyword._Pattern)
            {
                switch (_current.Value)
                {
                    case Keyword._class:
                        contents.Add(ParseClassDeclaration());
                        break;
                    case Keyword._func:
                        contents.Add(ParseMethodDeclaration());
                        break;
                    case Keyword._Pattern:
                        var test = ParsePatternDeclaration();
                        contents.Add(test);
                        break;
                }
            }
            if (_options.AllowRootStatements && _current != TokenKind.EndOfFile)
            {
                List<SyntaxNode> statements = new List<SyntaxNode>();

                var statementsStart = _current.Span.Start;

                while (_current != TokenKind.EndOfFile)
                {
                    statements.Add(ParseStatement());
                }

                contents.Add(new BlockStatement(CreateSpan(statementsStart), statements));
            }

            if (_current != TokenKind.EndOfFile)
            {
                AddError(Severity.Error, "Top-level statements are not permitted within the current options.", CreateSpan(_current.Span.Start, _tokens.Last().Span.End));
            }

            return new SourceDocument(CreateSpan(start), _sourceCode, contents);
        }

        private PatternDeclaration ParsePatternDeclaration()
        {
            var start = TakeKeyword(Keyword._Pattern);
            var name = ParseName();

            List<OptionDeclaration> options;
            List<SourceDeclaration> sources;
            ParseOptionList(out options, out sources);

            var body = ParseScope();

            if (sources.Count == 0)
                AddError(Severity.Error, "At least 1 Source is required.  Use the 'Source' keyword.", CreateSpan(_current.Span.Start, _tokens.Last().Span.End));

            var test = new PatternDeclaration(CreateSpan(start), name, options, sources, body);
            return test;
        }

        private void ParseOptionList(out List<OptionDeclaration> options, out List<SourceDeclaration> fileTypes)
        {
            options = new List<OptionDeclaration>();
            fileTypes = new List<SourceDeclaration>();
            var foundRepeating = false;
            var foundFirst = false;

            if (_current == TokenKind.Keyword && _current.Value == Keyword._Repeating)
                foundRepeating = true;

            if (_current == TokenKind.Keyword && _current.Value == Keyword._First)
                foundFirst = true;

            options.Add(ParseOptionDeclaration());
            while (_current == TokenKind.Comma)
            {
                Take(TokenKind.Comma);

                // Handle FileType
                if (_current == TokenKind.Keyword && _current.Value == Keyword._Source)
                {
                    Take(TokenKind.Keyword);
                    Take(TokenKind.LeftParenthesis);

                    fileTypes.Add(ParseFileTypeDeclaration());

                    while (_current == TokenKind.Comma)
                    {
                        Take(TokenKind.Comma);
                        fileTypes.Add(ParseFileTypeDeclaration());
                    }
                    Take(TokenKind.RightParenthesis);
                }
                else
                {
                    if (_current == TokenKind.Keyword && _current.Value == Keyword._Repeating)
                        foundRepeating = true;

                    if (_current == TokenKind.Keyword && _current.Value == Keyword._First)
                        foundFirst = true;

                    options.Add(ParseOptionDeclaration());
                }
            }

            if (foundRepeating && foundFirst)
                throw SyntaxError(Severity.Error, "You must select ONLY First or Repeating, not both!");
        }
        private SourceDeclaration ParseFileTypeDeclaration()
        {
            if (_current == TokenKind.Colon)
                Take();

            var option = Take();

            return new SourceDeclaration(CreateSpan(option), option.Value, option.Value);
        }
        private OptionDeclaration ParseOptionDeclaration()
        {
            if (_current == TokenKind.Colon)
                Take();

            if (_current != TokenKind.Keyword)
                throw UnexpectedToken($"{Keyword.PatternKeywords.ToOr()}");

            var token = Take();

            List<string> parameters = new List<string>();
            var opt = new OptionDeclaration(CreateSpan(token), token.Value, token.Value, parameters);
            token = ParseParameters(token, parameters);

            return opt;
        }



    }
}
