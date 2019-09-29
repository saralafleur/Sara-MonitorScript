using System.Linq;
using Sara.MonitorScript.Lexer;
using Xunit;

namespace Sara.MonitorScript.Test
{
    public class LexerTests
    {
        private readonly MonitorScriptLexer _lexer = new MonitorScriptLexer();

        [Fact]
        public void LexerScansComment()
        {
            const string program = "/*Hi*///Hello, World!!!";
            var tokens = _lexer.LexFile(program).ToArray();

            Assert.All(tokens, token => Assert.True(token.Catagory == TokenCatagory.Comment));
            Assert.True(tokens.First().Kind == TokenKind.BlockComment);
            Assert.True(tokens.Last().Kind == TokenKind.LineComment);
        }

        [Theory]
        [InlineData("9float")]
        [InlineData("1f.3")]
        [InlineData("2data")]
        [InlineData("\"")]
        public void LexerShouldOutputError(string value)
        {
            // ReSharper disable once UnusedVariable
            var token = _lexer.LexFile(value).ToArray();

            Assert.True(_lexer.ErrorSink.Count() == 1);

            _lexer.ErrorSink.Clear();

            Assert.True(!_lexer.ErrorSink.Any());
        }

        [Theory]
        [InlineData("1f")]
        [InlineData(".1f")]
        [InlineData(".1")]
        [InlineData("3.2e+10")]
        [InlineData(".1e-32")]
        [InlineData("0.0e0")]
        [InlineData(".1e0")]
        public void LexerShouldScanFloats(string value)
        {
            var token = _lexer.LexFile(value).First();

            Assert.True(token.Kind == TokenKind.FloatLiteral);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("21")]
        [InlineData("0130")]
        public void LexerShouldScanInteger(string value)
        {
            var token = _lexer.LexFile(value).First();

            Assert.True(token.Kind == TokenKind.IntegerLiteral);
        }

        [Theory]
        [InlineData("[]{}()", TokenCatagory.Grouping)]
        [InlineData("<>===-*/+^%&!|||&&", TokenCatagory.Operator)]
        [InlineData(".,;:", TokenCatagory.Punctuation)]
        [InlineData("var hello _09", TokenCatagory.Identifier)]
        public void TokensGroup(string value, TokenCatagory category)
        {
            var tokens = _lexer.LexFile(value).Where(g => g.Catagory != TokenCatagory.WhiteSpace).ToArray();

            Assert.All(tokens, token => Assert.True(token.Catagory == category));
            Assert.DoesNotContain(tokens, token => token.Kind == TokenKind.Error);
        }
    }
}
