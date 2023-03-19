﻿namespace Sharkie.CodeAnalysis;

using Binding;

internal sealed class Evaluator {
    public BoundExpression _root { get; }

    public Evaluator(BoundExpression root) {
        _root = root;
    }

    public object Evaluate() {
        return EvaluateExpression(_root);
    }

    private object EvaluateExpression(BoundExpression node) {
        if (node is BoundLiteralExpression n)
            return n.Value;

        if (node is BoundUnaryExpression u) {
            var operand =  EvaluateExpression(u.Operand);
            
            switch (u.Op.Kind) {
                case BoundUnaryOperatorKind.Identity:
                    return (int) operand;
                case BoundUnaryOperatorKind.Negation:
                    return -(int) operand;
                case BoundUnaryOperatorKind.LogicalNegation:
                    return !(bool)operand;
                default:
                    throw new Exception($"Unexpected unary operator {u.Op}");
            }
        }
        
        if (node is BoundBinaryExpression b) {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            switch (b.Op.Kind) {
                case BoundBinaryOperatorKind.Addition: return (int) left + (int) right;
                case BoundBinaryOperatorKind.Subtraction: return (int) left - (int) right;
                case BoundBinaryOperatorKind.Multiplication: return (int) left * (int) right;
                case BoundBinaryOperatorKind.Division: return (int) left / (int) right;
                case BoundBinaryOperatorKind.LogicalAND: return (bool) left && (bool) right;
                case BoundBinaryOperatorKind.LogicalOR: return (bool) left || (bool) right;
                default: throw new Exception($"Unexpected binary operator {b.Op}");
            }
        }
        
        throw new Exception($"Unexpected node {node.Kind}");
    }
}