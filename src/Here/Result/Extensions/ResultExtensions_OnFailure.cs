﻿using System;
using JetBrains.Annotations;

namespace Here.Results.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Result"/>, <see cref="Result{T}"/>, <see cref="CustomResult{TError}"/> and <see cref="Result{T, TError}"/> (On failure).
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result OnFailure(this Result result, [NotNull, InstantHandle] in Action onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailResult();
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <param name="result"><see cref="Result"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result"/>.</returns>
        [PublicAPI]
        public static Result OnFailure(this Result result, [NotNull, InstantHandle] in Action<Result> onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailResult();
            }

            return result;
        }

        #endregion

        #region Result<T>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] in Action onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailValueResult<T>();
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <param name="result"><see cref="Result{T}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T}"/> is failure.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T}"/>.</returns>
        [PublicAPI]
        public static Result<T> OnFailure<T>(this Result<T> result, [NotNull, InstantHandle] in Action<Result<T>> onFailure, in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailValueResult<T>();
            }

            return result;
        }

        #endregion

        #region CustomResult<TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Action onFailure,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Action onFailure,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorFactory());
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result,
            [NotNull, InstantHandle] in Action<CustomResult<TError>> onFailure,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="CustomResult{TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="CustomResult{TError}"/> is failure.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="CustomResult{TError}"/>.</returns>
        [PublicAPI]
        public static CustomResult<TError> OnFailure<TError>(this CustomResult<TError> result, 
            [NotNull, InstantHandle] in Action<CustomResult<TError>> onFailure,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomResult(errorFactory());
            }

            return result;
        }

        #endregion

        #region Result<T, TError>

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result, 
            [NotNull, InstantHandle] in Action onFailure,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] in Action onFailure,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure();

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorFactory());
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="errorObject">Custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result, 
            [NotNull, InstantHandle] in Action<Result<T, TError>> onFailure,
            [NotNull] in TError errorObject,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorObject);
            }

            return result;
        }

        /// <summary>
        /// Calls the <paramref name="onFailure"/> action when the <paramref name="result"/> is failure.
        /// </summary>
        /// <typeparam name="T">Result value type.</typeparam>
        /// <typeparam name="TError">Result custom error type.</typeparam>
        /// <param name="result"><see cref="Result{T, TError}"/> to check.</param>
        /// <param name="onFailure">Action to run if the <see cref="Result{T, TError}"/> is failure.</param>
        /// <param name="errorFactory">Function to create a custom error object.</param>
        /// <param name="treatWarningAsError">Flag to indicate how to treat warning (By default as success).</param>
        /// <returns>A <see cref="Result{T, TError}"/>.</returns>
        [PublicAPI]
        public static Result<T, TError> OnFailure<T, TError>(this Result<T, TError> result,
            [NotNull, InstantHandle] in Action<Result<T, TError>> onFailure,
            [NotNull, InstantHandle] in Func<TError> errorFactory,
            in bool treatWarningAsError = false)
        {
            if (IsConsideredFailure(result, treatWarningAsError))
            {
                onFailure(result);

                if (result.IsWarning)   // Warning as error
                    return result.ToFailCustomValueResult<T>(errorFactory());
            }

            return result;
        }

        #endregion
    }
}
