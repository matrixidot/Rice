namespace Sharkie.SyntaxNodes;

using CodeAnalysis;

sealed class NumberExpressionSyntax : ExpressionSyntax {
    public override SyntaxKind Kind => SyntaxKind.NumericalExpression;

    public override IEnumerable<SyntaxNode> GetChildren() {
        yield return NumberToken;
    }
    public SyntaxToken NumberToken { get; }

    public NumberExpressionSyntax(SyntaxToken numberToken) {
        NumberToken = numberToken;
    }


}