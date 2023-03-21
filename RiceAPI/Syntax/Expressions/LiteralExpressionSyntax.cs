namespace RiceAPI.Syntax.Expressions;

using System.Collections.Generic;

public sealed class LiteralExpressionSyntax : ExpressionSyntax {
    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
    public SyntaxToken LiteralToken { get; }
    public object Value { get; }

    public LiteralExpressionSyntax(SyntaxToken literalToken, object value) {
        LiteralToken = literalToken;
        Value = value;
    }
    
    public LiteralExpressionSyntax(SyntaxToken literalToken) : this(literalToken, literalToken.Value) {
    }
    
    public override IEnumerable<SyntaxNode> GetChildren() {
        yield return LiteralToken;
    }





}