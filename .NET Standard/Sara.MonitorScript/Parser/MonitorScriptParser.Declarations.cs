﻿using System.Collections.Generic;
using Sara.MonitorScript.Lexer;
using Sara.MonitorScript.Syntax;
using Sara.MonitorScript.Syntax.Declarations;
using Sara.MonitorScript.Syntax.Expressions;
using Sara.MonitorScript.Syntax.Statements;

namespace Sara.MonitorScript.Parser
{
    public sealed partial class MonitorScriptParser
    {
        private ClassDeclaration ParseClassDeclaration()
        {
            List<ConstructorDeclaration> cotrs = new List<ConstructorDeclaration>();
            List<PropertyDeclaration> props = new List<PropertyDeclaration>();
            List<MethodDeclaration> methods = new List<MethodDeclaration>();
            List<FieldDeclaration> fields = new List<FieldDeclaration>();

            var start = TakeKeyword(Keyword._class);
            var name = ParseName();

            MakeBlock(() =>
            {
                var member = ParseClassMember();
                switch (member?.Kind)
                {
                    case SyntaxKind.PropertyDeclaration:
                        props.Add(member as PropertyDeclaration);
                        break;

                    case SyntaxKind.FieldDeclaration:
                        fields.Add(member as FieldDeclaration);
                        break;

                    case SyntaxKind.MethodDeclaration:
                        methods.Add(member as MethodDeclaration);
                        break;

                    case SyntaxKind.ConstructorDeclaration:
                        cotrs.Add(member as ConstructorDeclaration);
                        break;
                }
            });

            return new ClassDeclaration(CreateSpan(start), name, cotrs, fields, methods, props);
        }

        private SyntaxNode ParseClassMember()
        {
            switch (_current.Value)
            {
                case "prop":
                    return ParsePropertyDeclaration();

                case "func":
                    return ParseMethodDeclaration();

                case "cotr":
                    return ParseConstructorDeclaration();

                default:
                    return ParseFieldDeclaration();
            }
        }

        private ConstructorDeclaration ParseConstructorDeclaration()
        {
            var start = TakeKeyword("cotr");
            var name = ParseName();

            var parameters = ParseParameterList();
            var body = ParseScope();

            return new ConstructorDeclaration(CreateSpan(start), name, parameters, body);
        }

        private FieldDeclaration ParseFieldDeclaration()
        {
            var start = _current.Span.Start;

            var type = ParseName();
            var name = ParseName();
            Expression defaultValue = null;
            if (_current == TokenKind.Equal)
            {
                Take();
                defaultValue = ParseExpression();
            }

            return new FieldDeclaration(CreateSpan(start), name, type, defaultValue);
        }

        private BlockStatement ParseLambdaOrScope()
        {
            if (_current == TokenKind.FatArrow)
            {
                Take();
                var expr = ParseExpression();
                TakeSemicolon();
                return new BlockStatement(expr.Span, new[] { expr });
            }
            else
            {
                return ParseScope();
            }
        }

        private MethodDeclaration ParseMethodDeclaration()
        {
            var start = TakeKeyword("func");
            var name = ParseName();
            var returnType = "Object";

            if (_current == TokenKind.LessThan)
            {
                returnType = ParseTypeAnnotation();
            }

            var parameters = ParseParameterList();

            var body = ParseLambdaOrScope();

            return new MethodDeclaration(CreateSpan(start), name, returnType, parameters, body);
        }

        private string ParseName()
        {
            if (_current.Kind == TokenKind.StringLiteral)
                return Take(TokenKind.StringLiteral).Value;
            else
                return Take(TokenKind.Identifier).Value;
        }

        private ParameterDeclaration ParseParameterDeclaration()
        {
            var name = Take(TokenKind.Identifier);
            var type = "Object";

            if (_current == TokenKind.Colon)
            {
                Take();
                type = ParseName();
            }

            return new ParameterDeclaration(CreateSpan(name), name.Value, type);
        }

        private IEnumerable<ParameterDeclaration> ParseParameterList()
        {
            List<ParameterDeclaration> parameters = new List<ParameterDeclaration>();
            MakeBlock(() =>
            {
                if (_current == TokenKind.RightParenthesis)
                {
                    return;
                }
                parameters.Add(ParseParameterDeclaration());
                while (_current == TokenKind.Comma)
                {
                    Take(TokenKind.Comma);
                    parameters.Add(ParseParameterDeclaration());
                }
            }, TokenKind.LeftParenthesis, TokenKind.RightParenthesis);

            return parameters;
        }

        private PropertyDeclaration ParsePropertyDeclaration()
        {
            var start = TakeKeyword("prop");
            var type = ParseTypeAnnotation();
            var name = ParseName();

            MethodDeclaration getMethod = null;
            MethodDeclaration setMethod = null;

            if (_current == TokenKind.FatArrow)
            {
                getMethod = ParseLambdaExpression(_current.Span.Start, new ParameterDeclaration[0]).ToMethodDeclaration($"get_{name}");
            }
            else
            {
                MakeBlock(() =>
                {
                    switch (_current.Value)
                    {
                        case "get":
                            {
                                var getStart = Take();
                                var body = ParseLambdaOrScope();
                                if (getMethod != null)
                                {
                                    AddError(Severity.Error, "Multiple getters", CreateSpan(getStart));
                                }
                                else
                                {
                                    getMethod = new MethodDeclaration(CreateSpan(getStart), $"get_{name}", type, new ParameterDeclaration[0], body);
                                }
                                break;
                            }
                        case "set":
                            {
                                var setStart = Take();
                                var body = ParseLambdaOrScope();
                                if (setMethod != null)
                                {
                                    AddError(Severity.Error, "Multiple setters", CreateSpan(setStart));
                                }
                                else
                                {
                                    setMethod = new MethodDeclaration(CreateSpan(setStart), $"set_{name}", "void",
                                                                      new[] { new ParameterDeclaration(setStart.Span, "value", type) }, body);
                                }
                                break;
                            }
                        default:
                            throw UnexpectedToken("get or set");
                    }
                });
            }
            if (getMethod == null)
            {
                AddError(Severity.Error, $"Property \"{name}\" does not have a getter!", CreateSpan(start));
            }
            return new PropertyDeclaration(CreateSpan(start), name, type, getMethod, setMethod);
        }

        private string ParseTypeAnnotation()
        {
            if (_current != TokenKind.LessThan)
            {
                throw UnexpectedToken("Type Annotation");
            }
            Take(TokenKind.LessThan);
            var identifier = ParseName();
            Take(TokenKind.GreaterThan);

            return identifier;
        }

        private VariableDeclaration ParseVariableDeclaration()
        {
            var start = TakeKeyword("var");
            var name = ParseName();
            var type = "MonitorScriptObject";
            Expression value = null;

            if (_current == TokenKind.Colon)
            {
                Take();
                type = ParseName();
            }
            if (_current == TokenKind.Assignment)
            {
                Take();

                value = ParseExpression();
            }

            return new VariableDeclaration(CreateSpan(start), name, type, value);
        }
    }
}
