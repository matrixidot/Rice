﻿namespace Sharkie.Binding;

using Syntax;

internal sealed class BoundBinaryOperator {
    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type) : this(syntaxKind, kind, type, type, type) { }

    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type resultType) {
        SyntaxKind = syntaxKind;
        Kind = kind;
        LeftType = leftType;
        RightType = rightType;
        ResultType = resultType;
    }
    public SyntaxKind SyntaxKind { get; }
    public BoundBinaryOperatorKind Kind { get; }
    public Type LeftType { get; }
    public Type RightType { get; }
    public Type ResultType { get; }

    private static BoundBinaryOperator[] _operators =
    { 
        new (SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
        new (SyntaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
        new (SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
        new (SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),
        
        new (SyntaxKind.DualAmpersandToken, BoundBinaryOperatorKind.LogicalAND, typeof(bool)),
        new (SyntaxKind.DualPipeToken, BoundBinaryOperatorKind.LogicalOR, typeof(bool)),

    };

    public static BoundBinaryOperator Bind(SyntaxKind syntaxKind, Type leftType, Type rightType) {
        foreach (var op in _operators) {
            if (op.SyntaxKind == syntaxKind && op.LeftType == leftType && op.RightType == rightType) return op;
        }
        return null;
    }

}