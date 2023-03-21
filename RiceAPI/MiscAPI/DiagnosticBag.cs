namespace BZAPI.MiscAPI;

using System.Collections;

using Syntax;

internal sealed class DiagnosticBag : IEnumerable<Diagnostic> {
    public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    private readonly List<Diagnostic> _diagnostics = new();
    
    public void AddRange(DiagnosticBag lexerDiagnostics) {
        _diagnostics.AddRange(lexerDiagnostics._diagnostics);
    }

    private void Report(TextSpan span, string message) {
        var diagnostic = new Diagnostic(span, message);
        _diagnostics.Add(diagnostic);
    }
    
    public void ReportInvalidNumber(TextSpan span, string text, Type type) {
        var message = $"The number {text} isn't a valid {type}.";
        Report(span, message);
    }

    public void ReportBadCharacter(int position, char character) {
        var message = $"Bad character input: '{character}'.";
        Report(new TextSpan(position, 1), message);
    }


    public void ReportUnexpectedToken(TextSpan span, SyntaxKind expectedKind, SyntaxKind actualKind) {
        var message = $"Unexpected token <{actualKind}>, expected <{expectedKind}>.";
        Report(span, message);
    }

    public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType) {
        var message = $"Unary operator '{operatorText}' is not defined for type {operandType}.";
        Report(span, message);
    }

    public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType) {
        var message = $"Binary operator '{operatorText}' is not defined for type {leftType} and {rightType}.";
        Report(span, message);
    }
}
