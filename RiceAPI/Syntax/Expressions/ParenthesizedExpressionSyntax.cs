namespace RiceAPI.Syntax.Expressions;

using System.Collections.Generic;

public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax {
    public override SyntaxKind Kind => SyntaxKind.ParentheticalExpression;
    public SyntaxToken OpenParenthesis { get; }
    public ExpressionSyntax Expression { get; }
    public SyntaxToken CloseParenthesis { get; }

    public ParenthesizedExpressionSyntax(SyntaxToken openParenthesis, ExpressionSyntax expression, SyntaxToken closeParenthesis) {
        OpenParenthesis = openParenthesis;
        Expression = expression;
        CloseParenthesis = closeParenthesis;
    }



    public override IEnumerable<SyntaxNode> GetChildren() {
        yield return OpenParenthesis;
        yield return Expression;
        yield return CloseParenthesis;
    }
}