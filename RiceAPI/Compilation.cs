namespace RiceAPI.CodeAnalysis;

using Binding;

using Syntax;

public sealed class Compilation {
    public SyntaxTree Syntax { get; }

    public Compilation(SyntaxTree syntax) {
        Syntax = syntax;
    }

    public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables) {
        var binder = new Binder(variables);
        var boundExpression = binder.BindExpression(Syntax.Root);

        var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics);
        if (diagnostics.Any())
            return new EvaluationResult(diagnostics, null);
        
        var evaluator = new Evaluator(boundExpression, variables);
        var value = evaluator.Evaluate();
        return new EvaluationResult(Array.Empty<Diagnostic>(), value);
    }
}