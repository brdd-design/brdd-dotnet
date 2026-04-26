using System;
using System.Collections.Generic;
using System.Linq;

namespace Brdd.Design.Core
{
    /// <summary>
    /// Standardized error object for BRDD.
    /// </summary>
    public record BrddError(string Code, string Message);

    /// <summary>
    /// A subset of the context that allows adding errors and checking validity.
    /// </summary>
    public interface IValidationContext
    {
        IReadOnlyList<BrddError> Errors { get; }
        void AddError(string code, string message);
        bool IsValid();
    }

    /// <summary>
    /// The central state object passed around and returned by UseCases.
    /// </summary>
    public interface IExecutionContext<T> : IValidationContext
    {
        T? Data { get; }
        IReadOnlyList<string> Setters { get; }
        IReadOnlyList<string> Effects { get; }
        int Status { get; }

        void AddEffect(string code);
        void AddSetter(string code);
        void SetData(T data);
    }

    /// <summary>
    /// The default implementation of the ExecutionContext.
    /// </summary>
    public class DefaultExecutionContext<T> : IExecutionContext<T>
    {
        public T? Data { get; private set; }
        private readonly List<BrddError> _errors = new();
        public IReadOnlyList<BrddError> Errors => _errors;
        
        private readonly List<string> _setters = new();
        public IReadOnlyList<string> Setters => _setters;
        
        private readonly List<string> _effects = new();
        public IReadOnlyList<string> Effects => _effects;
        
        public int Status { get; private set; } = 200;

        public DefaultExecutionContext(T? initialData = default)
        {
            Data = initialData;
        }

        public void AddError(string code, string message)
        {
            _errors.Add(new BrddError(code, message));
            Status = 400; // Default to Bad Request
        }

        public void AddEffect(string code)
        {
            _effects.Add(code);
        }

        public void AddSetter(string code)
        {
            _setters.Add(code);
        }

        public void SetData(T data)
        {
            Data = data;
        }

        public bool IsValid() => !_errors.Any();
    }

    /// <summary>
    /// Protocol for services dedicated to pure business logic validation.
    /// </summary>
    public interface IValidateService<in TInput>
    {
        void Validate(IValidationContext context, TInput input);
    }

    /// <summary>
    /// Protocol for services that fetch additional data needed for the UseCase.
    /// </summary>
    public interface IEnrichService<in TInput, out TEnriched, TContext>
    {
        TEnriched Enrich(IExecutionContext<TContext> context, TInput input);
    }

    /// <summary>
    /// Protocol for external adapters (APIs, DBs) to perform side-effects.
    /// </summary>
    public interface IClientService<in TInput, TContext>
    {
        void Execute(IExecutionContext<TContext> context, TInput input);
    }

    /// <summary>
    /// The orchestrator.
    /// </summary>
    public interface IUseCase<in TInput, TOutput>
    {
        IExecutionContext<TOutput> Execute(TInput input);
    }
}
