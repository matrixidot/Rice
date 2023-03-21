namespace RiceAPI.Syntax;

using System.Collections.Generic;
using System.Linq;

using CodeAnalysis;

using Expressions;

using MiscAPI;

public sealed class SyntaxTree
{
    public IReadOnlyList<Diagnostic> Diagnostics;
    public ExpressionSyntax Root { get; }
    public SyntaxToken EndOfFileToken { get; }

    public SyntaxTree(IEnumerable<Diagnostic> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken) {
        Diagnostics = diagnostics.ToArray();
        Root = root;
        EndOfFileToken = endOfFileToken;
    }

    public static SyntaxTree Parse(string text) {
        var parser = new Parser(text);
        return parser.Parse();
    }
}