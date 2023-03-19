﻿namespace Sharkie.SyntaxNodes;

using CodeAnalysis;

public sealed class LiteralExpressionSyntax : ExpressionSyntax {
    public override SyntaxKind Kind => SyntaxKind.NumericalExpression;
    public SyntaxToken LiteralToken { get; }
    
    public LiteralExpressionSyntax(SyntaxToken literalToken) {
        LiteralToken = literalToken;
    }
    
    public override IEnumerable<SyntaxNode> GetChildren() {
        yield return LiteralToken;
    }





}