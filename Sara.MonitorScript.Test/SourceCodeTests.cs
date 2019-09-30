using System;
using System.Linq;
using Xunit;
using static Xunit.Assert;

namespace Sara.MonitorScript.Test
{
    public class SourceCodeTests
    {
        private const string Program =
@"var name = 'Me';
name == 'Me';
return name;
// Final Line";

        private readonly SourceCode _code = new SourceCode(Program);

        [Fact]
        public void ContentsMatch()
        {
            Equal(Program, _code.Contents);
        }

        [Fact]
        public void SourceCodeHandlesOneSubset()
        {
            string[] selectedLines = _code.GetLines(1, 1);

            True(selectedLines.Length == 1);
            Equal(_code.GetLine(1), selectedLines.First());
        }

        [Fact]
        public void SourceCodeShouldReturnCorrectLine()
        {
            True(_code.GetLine(1).Substring(0, 3) == "var");

            True(_code.GetLine(4).StartsWith("//"));

            False(_code.GetLine(2).Contains("return"));
        }

        [Fact]
        public void SourceCodeShouldReturnCorrectSubset()
        {
            string[] selectedLines = _code.GetLines(2, 4);

            True(selectedLines.Length == 3);

            DoesNotContain(_code.GetLine(1), selectedLines);
            Contains(_code.GetLine(2), selectedLines);
        }

        [Fact]
        public void SubsetThrowsOutOfRange()
        {
            Throws<IndexOutOfRangeException>(() => _code.GetLines(0, int.MaxValue));
            Throws<IndexOutOfRangeException>(() => _code.GetLine(0));
            Throws<IndexOutOfRangeException>(() => _code.GetLine(-1));
        }
    }
}
