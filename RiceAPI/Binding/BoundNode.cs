namespace BZAPI.Binding;

using OperatorKinds;

internal abstract class BoundNode {
    public abstract BoundNodeKind Kind { get; }
}