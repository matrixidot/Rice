namespace RiceAPI.CodeAnalysis;

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
        var start = _position;
        /* ===== NUMBERS ===== */
        if (char.IsDigit(Current)) {
            

            while (char.IsDigit(Current)) Next();

            var length = _position - start;
            var text = _text.Substring(start, length);
            if (!int.TryParse(text, out var value))
                _diagnostics.ReportInvalidNumber(new TextSpan(start, length), _text, typeof(int));

            return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
        }
        
        /* ===== WHITESPACE ===== */
        if (char.IsWhiteSpace(Current)) {

            while (char.IsWhiteSpace(Current))
                Next();
            var length = _position - start;
            var text = _text.Substring(start, length);
            return new SyntaxToken(SyntaxKind.SpaceToken, start, text, null);
        }

        if (char.IsLetter(Current)) {

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
            case '/': return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            case '(': return new SyntaxToken(SyntaxKind.OpenParen, _position++, "(", null);
            case ')': return new SyntaxToken(SyntaxKind.CloseParen, _position++, ")", null);
            case '%': return new SyntaxToken(SyntaxKind.ModulusToken, _position++, "%", null);
            case '*': 
                if (LookAhead == '*') {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.ExponentToken, start, "**", null);
                }
                else {
                    _position++;
                    return new SyntaxToken(SyntaxKind.StarToken, start, "*", null);
                } 
            case '&':
                if (LookAhead == '&') {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.DualAmpersandToken, start, "&&", null);
                }
                break;
            case '|':
                if (LookAhead == '|') {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.DualPipeToken, start, "||", null);
                }
                break;            
            case '=':
                if (LookAhead == '=') {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.DualEqualsToken, start, "==", null);
                }
                else {
                    _position++;
                    return new SyntaxToken(SyntaxKind.EqualsToken, start, "=", null);
                }          
            case '!':
                if (LookAhead == '=') {
                    _position += 2;
                    return new SyntaxToken(SyntaxKind.BangEqualsToken, start, "!=", null);
                }
                else {
                    _position++;
                    return new SyntaxToken(SyntaxKind.BangToken, start, "!", null);
                }
        }

        _diagnostics.ReportBadCharacter(_position, Current);
        return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1), null);
    }
    private void Next() {
        _position++;
    }
}