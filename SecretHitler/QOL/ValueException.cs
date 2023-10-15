using System.Diagnostics.CodeAnalysis;

namespace SecretHitler.QOL;

/// <summary>
/// A class that contains a value that is checked against specified constraints to ensure it stays valid
/// as per user specification.
/// </summary>
/// <param name="val">The initial value, this can be changed with the setter on <see cref="Value"/></param>
/// <param name="exception">
/// The exception that should be thrown if the value is read while invalid. <see cref="TryGet"/> won't throw
/// this exception, and checking can be ignored with <see cref="Ignore"/>. The stored value is immutable.
/// </param>
/// <param name="exceptionConditions">
/// The specified constraints that return true if the provided value is invalid. The stored value is immutable.
/// </param>
/// <typeparam name="T">The type of <paramref name="val"/>/<see cref="Value"/></typeparam>
public sealed class ValueException<T>(T val, Exception exception, params ComparisonMethod<T>[] exceptionConditions)
{
    private readonly object _ioLock = new();
    
    private T _value = val;
    public T Value
    {
        private get => _value;
        set
        {
            lock (_ioLock)
            {
                _checkedResult = null;
                _value = value;
            }
        }
    }

    /// <summary>
    /// Null if unchecked, otherwise use the bool for the result.
    /// </summary>
    private bool? _checkedResult;
    
    public Exception Exception { get; } = exception;
    public IEnumerable<ComparisonMethod<T>> ExceptionConditions { get; } = exceptionConditions;

    public static implicit operator T(ValueException<T> warned) => warned.Get();
    
    public bool HasException
    {
        get
        {
            lock (_ioLock)
            {
                _checkedResult ??= ExceptionConditions.Any(condition => condition(val));
                return _checkedResult.Value;
                
            }
        }
    }

    public Exception? GetExceptionIfHasException() => HasException ? Exception : null;

    public T Get() {
        if (HasException)
            throw Exception;

        return Value;
    }

    public bool TryGet([NotNullWhen(true)] out T? outVal)
    {
        outVal = HasException ? default : Value;
        return outVal is not null;
    }

    public bool ExceptionIfComparedTo(ComparisonMethod<T> comparison) => FailedComparisons(comparison);
    public bool ExceptionIfComparedTo(params ComparisonMethod<T>[] comparisons) 
        => FailedComparisons();
    
    public bool TryGetIfNoException([NotNullWhen(true)] out T? outVal, ComparisonMethod<T> comparison)
    {
        bool failed = FailedComparisons(comparison) || HasException;

        outVal = failed ? default : Value;
        return !failed;
    }

    public bool TryGetIfNoException([NotNullWhen(true)] out T? outVal, params ComparisonMethod<T>[] comparisons)
    {
        bool failed = FailedComparisons() || HasException;

        outVal = failed ? default : Value;
        return !failed;
    }

    public bool GetIfComparison([NotNullWhen(true)] out T? outVal, ComparisonMethod<T> comparison)
    {
        if (FailedComparisons(comparison))
        {
            outVal = default;
            return false;
        }

        outVal = Get()!;

        return true;
    }

    public bool GetIfComparisons([NotNullWhen(true)] out T? outVal, params ComparisonMethod<T>[] comparisons)
    {
        if (FailedComparisons())
        {
            outVal = default;
            return false;
        }

        outVal = Get()!;

        return true;
    }

    public T Ignore() => Value;

    private bool FailedComparisons(params ComparisonMethod<T>[] comparisons)
    {
        bool failed = HasException;
        
        lock (_ioLock)
        {
            failed = failed || comparisons.Any(comparison => comparison(Value));
        }

        return failed;
    }
}

public static class ValueExceptionCreator
{
    public static ValueException<T> CreateValueException<T>(this T val, Exception exception, 
        params ComparisonMethod<T>[] conditions) => new(val, exception, conditions);
} 

/// <summary>
/// Treat the comparison like it's:
/// <code>
/// if (ComparisonMethod(val)) throw ...
/// </code>
/// </summary>
public delegate bool ComparisonMethod<in T>(T val);