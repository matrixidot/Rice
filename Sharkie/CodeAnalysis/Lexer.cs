namespace Sharkie.CodeAnalysis;

public class Lexer {
    private readonly string _text;
    private int _position;
    private readonly List<string> _diagnostics = new();
    public IEnumerable<string> Diagnostics => _diagnostics;
    private char Current => _position >= _text.Length ? '\0' : _text[_position];

    public Lexer(string text) {
        _text = text;
    }
    
    public SyntaxToken NextToken() {
        if (_position >= _text.Length)
            return new SyntaxToken(SyntaxKind.END, _position, "\0", null);
        
        /* ===== NUMBERS ===== */
        if (char.IsDigit(Current)) {
            var start = _position;

            while (char.IsDigit(Current)) Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            if (!int.TryParse(text, out var value))
                _diagnostics.Add($"The number {_text} is not a valid int32");

            return new SyntaxToken(SyntaxKind.NUMBER, start, text, value);
        }
        
        /* ===== WHITESPACE ===== */
        if (char.IsWhiteSpace(Current)) {
            var start = _position;

            while (char.IsWhiteSpace(Current))
                Next();
            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.SPACE, start, text, null);
        }
        
        /* ===== OPERATORS ===== */
        switch (Current) {
            case '+': return new SyntaxToken(SyntaxKind.PLUS, _position++, "+", null);
            case '-': return new SyntaxToken(SyntaxKind.MINUS, _position++, "-", null);
            case '*': return new SyntaxToken(SyntaxKind.STAR, _position++, "*", null);
            case '/': return new SyntaxToken(SyntaxKind.SLASH, _position++, "/", null);
            case '(': return new SyntaxToken(SyntaxKind.OpenParen, _position++, "(", null);
            case ')': return new SyntaxToken(SyntaxKind.CloseParen, _position++, ")", null);
        }

        _diagnostics.Add($"ERROR: bad character input: '{Current}'");
        return new SyntaxToken(SyntaxKind.BAD, _position++, _text.Substring(_position - 1, 1), null);
    }
    private void Next() {
        _position++;
    }
}