namespace RiceAPI.Binding.Expressions;

using System;

internal abstract class BoundExpression : BoundNode {
    public abstract Type Type { get; }
}