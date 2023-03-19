namespace Sharkie.CodeAnalysis;

public enum SyntaxKind
{
    // TOKENS
    BAD,
    END,
    SPACE,
    NUMBER,
    // OPERATORS
    PLUS,
    MINUS,
    STAR,
    SLASH,
    OpenParen,
    CloseParen,
    // EXPRESSIONS
    BinaryExpression,
    NumericalExpression,
    ParentheticalExpression,
}