namespace RiceAPI.Syntax;

using System.Collections.Generic;

public abstract class SyntaxNode
{
    public abstract SyntaxKind Kind { get; }

    public abstract IEnumerable<SyntaxNode> GetChildren();

}