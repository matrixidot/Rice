namespace Sharkie.CodeAnalysis;

using SyntaxNodes;

internal sealed class Parser {
    private readonly SyntaxToken[] _tokens;
    private int _position;
    private readonly List<string> _diagnostics = new();

    public Parser(string text) {
        var tokens = new List<SyntaxToken>();
        var lexer = new Lexer(text);
        SyntaxToken token;
        
        do {
            token = lexer.NextToken();

            if (token.Kind is not SyntaxKind.SPACE or SyntaxKind.BAD)
                tokens.Add(token);

        } while (token.Kind is not SyntaxKind.END);

        _tokens = tokens.ToArray();
        _diagnostics.AddRange(lexer.Diagnostics);
    }
    
    public SyntaxTree Parse() {
        var expression = ParseTerm();
        var endOfFileToken = MatchToken(SyntaxKind.END);
        return new SyntaxTree(_diagnostics, expression, endOfFileToken);
    }

    private ExpressionSyntax ParseTerm() {
        var left = ParseFactor();

        while (Current.Kind is SyntaxKind.PLUS or SyntaxKind.MINUS) {
            var operatorToken = NextToken();
            var right = ParseFactor();
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }

        return left;
    }

    private ExpressionSyntax ParseFactor() {
        var left = ParsePrimaryExpression();

        while (Current.Kind is SyntaxKind.STAR or SyntaxKind.SLASH) {
            var operatorToken = NextToken();
            var right = ParsePrimaryExpression();
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }

        return left;
    }

    private ExpressionSyntax ParsePrimaryExpression() {
        if (Current.Kind == SyntaxKind.OpenParen) {
            var left = NextToken();
            var expression = ParseExpression();
            var right = MatchToken(SyntaxKind.CloseParen);
            return new ParenthesizedExpressionSyntax(left, expression, right);
        }

        var numberToken = MatchToken(SyntaxKind.NUMBER);
        return new LiteralExpressionSyntax(numberToken);
    }

    private SyntaxToken Peek(int offset) {
        var index = _position + offset;
        if (index >= _tokens.Length)
            return _tokens[^1];

        return _tokens[index];
    }

    private SyntaxToken NextToken() {
        var current = Current;
        _position++;
        return current;
    }

    private SyntaxToken MatchToken(SyntaxKind kind) {
        if (Current.Kind == kind) 
            return NextToken();
        _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}>, expected <{kind}>");
        return new SyntaxToken(kind, Current.Position, null, null);
    }

    private ExpressionSyntax ParseExpression() { return ParseTerm(); }

    private SyntaxToken Current => Peek(0);
}