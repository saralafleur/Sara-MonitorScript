using System;
using System.Linq;
using Sara.MonitorScript.Lexer;
using Sara.MonitorScript.Parser;

namespace Sara.MonitorScript.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MonitorScriptLexer lexer = new MonitorScriptLexer();
            MonitorScriptParser parser = new MonitorScriptParser(lexer.ErrorSink);

            System.Console.Write("MonitorScript> ");
            var program = @"Pattern FindWhiteVideo Repeating, TELLER, Something, George
{ 
	Start Event
	Body Event 
	Body Event Optional
	Stop Event 
	Reset Event 
}";

//            program = @"func test (string three)
//{ 
//    var one = 1;
//    var two = 2;
//}";


            //var program = Console.ReadLine();
            var sourceCode = new SourceCode(program);
            var tokens = lexer.LexFile(sourceCode).ToArray();

            foreach (var token in tokens)
            {
                System.Console.WriteLine($"{token.Kind} ( \"{token.Value.Replace("\n", "\\n").Replace("\r", "\\r")}\" ) ");
            }

            if (lexer.ErrorSink.Count() > 0)
            {
                foreach (var error in lexer.ErrorSink)
                {
                    System.Console.WriteLine(new string('-', System.Console.WindowWidth / 3));

                    WriteError(error);
                }
                lexer.ErrorSink.Clear();
            }
            else
            {
                // Abstract syntax tree
                var ast = parser.ParseFile(sourceCode, tokens, MonitorScriptParserOptions.OptionalSemicolons);
                if (lexer.ErrorSink.Count() > 0)
                {
                    foreach (var error in lexer.ErrorSink)
                    {
                        System.Console.WriteLine(new string('-', System.Console.WindowWidth / 3));

                        WriteError(error);
                    }
                    lexer.ErrorSink.Clear();
                }
            }

            System.Console.WriteLine(new string('-', System.Console.WindowWidth / 2));



            System.Console.WriteLine("END OF PROGRAM");
            System.Console.ReadLine();
        }

        private static void WriteError(ErrorEntry error)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            if (error.Lines.Length > 1)
            {
                System.Console.WriteLine(error.Lines.First());
                System.Console.CursorLeft = error.Span.Start.Column;
                System.Console.WriteLine(new string('^', error.Lines[0].Length - error.Span.Start.Column));
                for (int i = 1; i < error.Lines.Length - 1; i++)
                {
                    System.Console.WriteLine(error.Lines[i]);
                    System.Console.WriteLine(new string('^', error.Lines[i].Length));
                }
                System.Console.WriteLine(error.Lines.Last());
                System.Console.WriteLine(new string('^', error.Lines.Last().Length - error.Span.End.Column));
            }
            else
            {
                System.Console.WriteLine(error.Lines.First());
                System.Console.CursorLeft = error.Span.Start.Column;
                System.Console.WriteLine(new string('^', error.Span.Length));
                System.Console.WriteLine($"{error.Severity} {error.Span}: {error.Message}");
            }
            System.Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
