﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Here.Maybes.Extensions
{
    /// <summary>
    /// Extensions related to <see cref="Maybe{T}"/> for operations.
    /// </summary>
    public static class MaybeOperationsExtensions
    {
        /// <summary>
        /// Call the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> If<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<T> then)
        {
            if (maybe.HasValue)
                then(maybe.Value);
            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="onItem"/> function on each item if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> ForEachIf<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<object> onItem)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                    onItem(item);
            }

            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="onItem"/> function on each item if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> ForEachIf<T, TItem>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<TItem> onItem)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                    onItem(item);
            }

            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="else"/> function if this <see cref="Maybe{T}"/> has no value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="else">Treatment to do.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> Else<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action @else)
        {
            if (maybe.HasNoValue)
                @else();
            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise call <paramref name="else"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> IfElse<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<T> then, [NotNull, InstantHandle] Action @else)
        {
            if (maybe.HasValue)
                then(maybe.Value);
            else
                @else();

            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="onItem"/> function on each item if this <see cref="Maybe{T}"/> has a value, otherwise call <paramref name="else"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <param name="else">Treatment to do if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> ForEachIfElse<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<object> onItem, [NotNull] Action @else)
            where T : IEnumerable
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                    onItem(item);
            }
            else
                @else();

            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="onItem"/> function on each item if this <see cref="Maybe{T}"/> has a value, otherwise call <paramref name="else"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TItem">Enumerable item type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="onItem">Treatment to do on each item.</param>
        /// <param name="else">Treatment to do if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/>.</returns>
        [PublicAPI]
        public static Maybe<T> ForEachIfElse<T, TItem>(this Maybe<T> maybe, [NotNull, InstantHandle] Action<TItem> onItem, [NotNull] Action @else)
            where T : IEnumerable<TItem>
        {
            if (maybe.HasValue)
            {
                foreach (var item in maybe.Value)
                    onItem(item);
            }
            else
                @else();

            return maybe;
        }

        /// <summary>
        /// Call the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="else">Treatment to do if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        [PublicAPI, CanBeNull]
        public static TResult IfElse<T, TResult>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<T, TResult> then, [NotNull, InstantHandle] Func<TResult> @else)
        {
            if (maybe.HasValue)
                return then(maybe.Value);
            return @else();
        }

        /// <summary>
        /// Call the <paramref name="then"/> function if this <see cref="Maybe{T}"/> has a value, otherwise return the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="then">Treatment to do with this <see cref="Maybe{T}"/> value.</param>
        /// <param name="orValue">Value to return if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>Result of the treatment.</returns>
        [PublicAPI, CanBeNull]
        public static TResult IfOr<T, TResult>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<T, TResult> then, [NotNull] TResult orValue)
        {
            if (maybe.HasValue)
                return then(maybe.Value);
            return orValue;
        }

        /// <summary>
        /// Call the <paramref name="else"/> function if this <see cref="Maybe{T}"/> has no value, otherwise return the <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TResult">Type of the returned value.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> on which performing treatment.</param>
        /// <param name="else">Treatment to compute result value.</param>
        /// <param name="orValue">Value to return if this <see cref="Maybe{T}"/> has a value.</param>
        /// <returns>Result of the treatment.</returns>
        [PublicAPI, CanBeNull]
        public static TResult ElseOr<T, TResult>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<TResult> @else, [NotNull] TResult orValue)
        {
            if (maybe.HasNoValue)
                return @else();
            return orValue;
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has value, otherwise returns <paramref name="orValue"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="orValue">Value to use as fallback if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value, or <paramref name="orValue"/>.</returns>
        [PublicAPI, CanBeNull, Pure]
        public static T Or<T>(this Maybe<T> maybe, [CanBeNull] T orValue)
        {
            if (maybe.HasValue)
                return maybe.Value;
            return orValue;
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise returns a value from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="orFunc">Function called if the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value, or the result of <paramref name="orFunc"/>.</returns>
        [PublicAPI, CanBeNull, Pure]
        public static T Or<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<T> orFunc)
        {
            if (maybe.HasValue)
                return maybe.Value;
            return orFunc();
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise the default value of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <returns>This <see cref="Maybe{T}"/> value, or the default to <typeparamref name="T"/>.</returns>
        [PublicAPI, CanBeNull, Pure]
        public static T OrDefault<T>(this Maybe<T> maybe)
        {
            if (maybe.HasValue)
                return maybe.Value;
            return default(T);
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="exception">The exception to throw if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value.</returns>
        [PublicAPI, NotNull]
        public static T OrThrows<T>(this Maybe<T> maybe, [NotNull] Exception exception)
        {
            if (maybe.HasValue)
                return maybe.Value;
            throw exception;
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> value if it has one, otherwise throws the given exception.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="exceptionFunc">The factory exception method used to throw if this <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/> value.</returns>
        [PublicAPI, NotNull]
        public static T OrThrows<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<Exception> exceptionFunc)
        {
            if (maybe.HasValue)
                return maybe.Value;
            throw exceptionFunc();
        }

        /// <summary>
        /// Returns this <see cref="Maybe{T}"/> if it has a value, otherwise returns a <see cref="Maybe{T}"/> from <paramref name="orFunc"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to check.</param>
        /// <param name="orFunc">Function called if the <see cref="Maybe{T}"/> has no value.</param>
        /// <returns>This <see cref="Maybe{T}"/>, or the result of <paramref name="orFunc"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<T> Or<T>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<Maybe<T>> orFunc)
        {
            if (maybe.HasValue)
                return maybe;
            return orFunc();
        }

        /// <summary>
        /// Unwrap this <see cref="Maybe{T}"/> value if it has one, otherwise returns the default value.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to unwrap value.</param>
        /// <returns>The unwrapped value from this <see cref="Maybe{T}"/> if it has value, otherwise the default value.</returns>
        [PublicAPI, CanBeNull, Pure]
        public static T Unwrap<T>(this Maybe<T> maybe, [CanBeNull] T defaultValue = default(T))
        {
            return maybe.Or(defaultValue);
        }

        /// <summary>
        /// Unwrap this <see cref="Maybe{T}"/> value if it has one, otherwise returns a value from <paramref name="converter"/>.
        /// </summary>
        /// <typeparam name="T">Type of the value embedded in this <see cref="Maybe{T}"/>.</typeparam>
        /// <typeparam name="TOut">Output value type.</typeparam>
        /// <param name="maybe"><see cref="Maybe{T}"/> to unwrap value.</param>
        /// <param name="converter">Function called to convert this <see cref="Maybe{T}"/> value.</param>
        /// <returns>The unwrapped value from this <see cref="Maybe{T}"/> if it has value, otherwise the default value.</returns>
        [PublicAPI, CanBeNull, Pure]
        public static TOut Unwrap<T, TOut>(this Maybe<T> maybe, [NotNull, InstantHandle] Func<T, TOut> converter, [CanBeNull] TOut defaultValue = default(TOut))
        {
            if (maybe.HasValue)
                return converter(maybe.Value);
            return defaultValue;
        }

        /// <summary>
        /// Convert this <see cref="Maybe{TFrom}"/> if it has a value to a <see cref="Maybe{TTo}"/>.
        /// </summary>
        /// <typeparam name="TFrom">Type of the value embedded in this <see cref="Maybe{TFrom}"/>.</typeparam>
        /// <typeparam name="TTo">Type of the value embedded in the converted <see cref="Maybe{TTo}"/>.</typeparam>
        /// <param name="maybe"><see cref="Maybe{TFrom}"/> to convert.</param>
        /// <param name="converter">Function called to convert this <see cref="Maybe{TFrom}"/>.</param>
        /// <returns>The conversion of this <see cref="Maybe{TFrom}"/> to <see cref="Maybe{TTo}"/>.</returns>
        [PublicAPI, Pure]
        public static Maybe<TTo> Cast<TFrom, TTo>(this Maybe<TFrom> maybe, [NotNull, InstantHandle] Func<TFrom, TTo> converter)
        {
            if (maybe.HasValue)
                return converter(maybe.Value);
            return Maybe<TTo>.None;
        }
    }
}
