namespace RiceAPI.Syntax;

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
    EqualsToken,
    ExponentToken,
    ModulusToken,

    // EXPRESSIONS
    BinaryExpression,
    UnaryExpression,
    LiteralExpression,
    ParentheticalExpression,
    NameExpression,
    AssignmentExpression,

    // KEYWORDS
    FalseKeyword,
    TrueKeyword,

    // BOOLEAN OPERATORS
    BangToken,
    DualAmpersandToken,
    DualPipeToken,
    DualEqualsToken,
    BangEqualsToken,
}