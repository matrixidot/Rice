namespace Sharkie.Syntax;

public enum SyntaxKind
{
    // SPECIAL TOKENS
    BadToken,
    EndOfFileToken,
    SpaceToken,
    NumberToken,
    IdentifierToken,
    
    // OPERATOR TOKENS
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
    
    // KEYWORDS
    FalseKeyword,
    TrueKeyword,

    // BOOLEAN OPERATORS
    BangToken,
    DualAmpersandToken,
    DualPipeToken,
}