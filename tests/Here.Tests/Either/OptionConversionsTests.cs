﻿using System;
using NUnit.Framework;
using Here.Extensions;
using static Here.Tests.Eithers.EitherTestHelpers;

namespace Here.Tests.Eithers
{
    /// <summary>
    /// Tests for <see cref="Option{T}"/> conversions to either.
    /// </summary>
    [TestFixture]
    internal class OptionConversionsTests : HereTestsBase
    {
        [Test]
        public void OptionToEither()
        {
            // Option has value
            var optionInt = Option<int>.Some(42);
            Either<string, int> either = optionInt.ToEither("Error");
            CheckRightEither(either, 42);

            either = optionInt.ToEither(() => "Error");
            CheckRightEither(either, 42);

            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<ArgumentNullException>(() => optionInt.ToEither((string)null));
            Assert.Throws<ArgumentNullException>(() => optionInt.ToEither((Func<string>)null));
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed

            // Empty Option
            var emptyOptionInt = Option<int>.None;
            either = emptyOptionInt.ToEither("Error");
            CheckLeftEither(either, "Error");

            either = emptyOptionInt.ToEither(() => "Error 2");
            CheckLeftEither(either, "Error 2");

            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<ArgumentNullException>(() => emptyOptionInt.ToEither((string)null));
            Assert.Throws<ArgumentNullException>(() => emptyOptionInt.ToEither((Func<string>)null));
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }
    }
}