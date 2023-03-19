namespace Sharkie.CodeAnalysis;

using SyntaxNodes;

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
        
        if (node is BinaryExpressionSyntax b) {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            switch (b.OperatorToken.Kind) {
                case SyntaxKind.PLUS: return left + right;
                case SyntaxKind.MINUS: return left - right;
                case SyntaxKind.STAR: return left * right;
                case SyntaxKind.SLASH: return left / right;
                default: throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
            }
        }

        if (node is ParenthesizedExpressionSyntax p)
            return EvaluateExpression(p.Expression);

        throw new Exception($"Unexpected node {node.Kind}");
    }
}