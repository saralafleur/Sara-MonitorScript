namespace Sara.MonitorScript.Syntax
{
    public enum SyntaxKind
    {
        Invalid,
        SourceDocument,
        BinaryExpression,
        UnaryExpression,
        IdentifierExpression,
        ConstantExpression,
        ReferenceExpression,
        MethodCallExpression,
        ParameterDeclaration,
        BlockStatement,
        LambdaExpression,
        NewExpression,
        ArrayAccessExpression,
        WhileStatement,
        IfStatement,
        ElseStatement,
        SwitchStatement,
        CaseStatement,
        EmptyStatement,
        BreakStatement,
        ContinueStatement,
        ForStatement,
        VariableDeclaration,
        ClassDeclaration,
        FieldDeclaration,
        MethodDeclaration,
        ConstructorDeclaration,
        PropertyDeclaration,
        ReturnStatement,

        // new
        PatternDeclaration,
        OptionDeclaration,
        EventStatement,
        SourceDeclaration,
    }
}
