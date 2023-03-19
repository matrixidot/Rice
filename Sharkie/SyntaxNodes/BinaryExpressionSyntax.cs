﻿namespace Sharkie.SyntaxNodes;

using CodeAnalysis;

public sealed class BinaryExpressionSyntax : ExpressionSyntax {
    public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
    public ExpressionSyntax Left { get; }
    public SyntaxToken OperatorToken { get; }
    public ExpressionSyntax Right { get; }
    public override IEnumerable<SyntaxNode> GetChildren() {
        yield return Left;
        yield return OperatorToken;
        yield return Right;
    }
    
    public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right) {
        Left = left;
        OperatorToken = operatorToken;
        Right = right;
    }
}