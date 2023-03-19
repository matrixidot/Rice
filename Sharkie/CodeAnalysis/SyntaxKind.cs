namespace Sharkie.CodeAnalysis;

public enum SyntaxKind
{
    // TOKENS
    BadToken,
    EndOfFileToken,
    SpaceToken,
    NumberToken,
    // OPERATORS
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    OpenParen,
    CloseParen,
    // EXPRESSIONS
    BinaryExpression,
    UnaryExpression,
    LiteralExpression,
    ParentheticalExpression,

}