﻿namespace Sharkie.CodeAnalysis;

using Syntax;
using Syntax.Expressions;

public sealed class Evaluator {
    public ExpressionSyntax _root { get; }

    public Evaluator(ExpressionSyntax root) {
        _root = root;
    }

    public int Evaluate() {
        return EvaluateExpression(_root);
    }

    private int EvaluateExpression(ExpressionSyntax node) {
        if (node is LiteralExpressionSyntax n)
            return (int)n.LiteralToken.Value;

        if (node is UnaryExpressionSyntax u) {
            var operand = EvaluateExpression(u.Operand);
            if (u.OperatorToken.Kind == SyntaxKind.PlusToken)
                return operand;
            if (u.OperatorToken.Kind == SyntaxKind.MinusToken)
                return -operand;
            throw new Exception($"Unexpected unary operator {u.OperatorToken.Kind}");
        }
        
        if (node is BinaryExpressionSyntax b) {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            switch (b.OperatorToken.Kind) {
                case SyntaxKind.PlusToken: return left + right;
                case SyntaxKind.MinusToken: return left - right;
                case SyntaxKind.StarToken: return left * right;
                case SyntaxKind.SlashToken: return left / right;
                default: throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
            }
        }

        if (node is ParenthesizedExpressionSyntax p)
            return EvaluateExpression(p.Expression);

        throw new Exception($"Unexpected node {node.Kind}");
    }
}