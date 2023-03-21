namespace BZAPI.CodeAnalysis;

using MiscAPI;

using Syntax;

internal sealed class Lexer {
    private readonly string _text;
    private int _position;
    private DiagnosticBag _diagnostics = new();
    
    public DiagnosticBag Diagnostics => _diagnostics;

    private char Current => Peek(0);

    private char LookAhead => Peek(1);

    public Lexer(string text) {
        _text = text;
    }
    
    private char Peek(int offset) {
        var index = _position + offset;
        if (index >= _text.Length)
            return '\0';
        return _text[index];
    }


    
    public SyntaxToken Lex() {
        if (_position >= _text.Length)
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
        
        /* ===== NUMBERS ===== */
        if (char.IsDigit(Current)) {
            var start = _position;

            while (char.IsDigit(Current)) Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            if (!int.TryParse(text, out var value))
                _diagnostics.ReportInvalidNumber(new TextSpan(start, length), _text, typeof(int));

            return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
        }
        
        /* ===== WHITESPACE ===== */
        if (char.IsWhiteSpace(Current)) {
            var start = _position;

            while (char.IsWhiteSpace(Current))
                Next();
            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.SpaceToken, start, text, null);
        }

        if (char.IsLetter(Current)) {
            var start = _position;

            while (char.IsLetter(Current))
                Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            var kind = SyntaxFacts.GetKeywordKind(text);
            return new SyntaxToken(kind, start, text, null);
        }

        /* ===== OPERATORS ===== */
        switch (Current) {
            case '+': return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            case '-': return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            case '*': return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
            case '/': return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            case '(': return new SyntaxToken(SyntaxKind.OpenParen, _position++, "(", null);
            case ')': return new SyntaxToken(SyntaxKind.CloseParen, _position++, ")", null);
            case '&': if (LookAhead == '&') return new SyntaxToken(SyntaxKind.DualAmpersandToken, _position += 2, "&&", null);
                break;
            case '|': if (LookAhead == '|') return new SyntaxToken(SyntaxKind.DualPipeToken, _position += 2, "||", null);
                break;            
            case '=': if (LookAhead == '=') return new SyntaxToken(SyntaxKind.DualEqualsToken, _position += 2, "==", null);
                break;            
            case '!': 
                if (LookAhead == '=') return new SyntaxToken(SyntaxKind.BangEqualsToken, _position += 2, "!=", null);
                else return new SyntaxToken(SyntaxKind.BangToken, _position++, "!", null);
        }

        _diagnostics.ReportBadCharacter(_position, Current);
        return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
    }
    private void Next() {
        _position++;
    }
}