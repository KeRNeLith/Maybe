﻿using JetBrains.Annotations;
using Here.Extensions;

namespace Here
{
	// Implicit operators
    public partial struct Maybe<T>
    {
        /// <summary>
        /// Implicit constructor for an empty <see cref="Maybe{T}"/>.
        /// </summary>
        /// <returns>A None maybe.</returns>
        [PublicAPI, Pure]
        public static implicit operator Maybe<T>([NotNull] in Maybe.NoneClass none)
        {
            return None;
        }

        /// <summary>
        /// Implicit constructor of <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="value">Value to initialize the <see cref="Maybe{T}"/>.</param>
        /// <returns>A <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Maybe<T>([CanBeNull] in T value)
        {
            if (value == null)
                return None;

            return Some(value);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Maybe{Maybe}"/> to a <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="embeddedMaybe">A <see cref="Maybe{Maybe}"/>.</param>
        /// <returns>A <see cref="Maybe{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Maybe<T>(in Maybe<Maybe<T>> embeddedMaybe)
        {
            return embeddedMaybe.Flatten();
        }

        /// <summary>
        /// Implicit conversion from <see cref="Maybe{T}"/> to a boolean.
        /// </summary>
        /// <param name="maybe"><see cref="Maybe{T}"/> to convert.</param>
        /// <returns>A corresponding boolean.</returns>
        [PublicAPI, Pure]
        public static implicit operator bool(in Maybe<T> maybe)
        {
            return maybe.HasValue;
        }

        /// <summary>
        /// Implicit conversion from <see cref="Maybe{T}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="maybe">A <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>The corresponding <see cref="Result"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result(in Maybe<T> maybe)
        {
            return maybe.ToResult();
        }

        /// <summary>
        /// Implicit conversion from <see cref="Maybe{T}"/> to a <see cref="Result{T}"/>.
        /// </summary>
        /// <param name="maybe">A <see cref="Maybe{T}"/> to convert.</param>
        /// <returns>The corresponding <see cref="Result{T}"/>.</returns>
        [PublicAPI, Pure]
        public static implicit operator Result<T>(in Maybe<T> maybe)
        {
            return maybe.ToValueResult();
        }
    }
}
