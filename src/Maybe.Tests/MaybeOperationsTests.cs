﻿using Here.Maybes.Extensions;
using NUnit.Framework;

namespace Here.Maybes.Tests
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeOperationsTests : MaybeTestsBase
    {
        [Test]
        public void MaybeIf()
        {
            int counter = 0;
            var maybeInt = Maybe<int>.Some(12);
            maybeInt.If(value => ++counter);
            Assert.AreEqual(1, counter);

            var emptyMaybeInt = Maybe<int>.None;
            emptyMaybeInt.If(value => ++counter);
            Assert.AreEqual(1, counter);
        }

        [Test]
        public void MaybeIfElse()
        {
            int counterIf = 0;
            int counterElse = 0;
            var maybeInt = Maybe<int>.Some(12);
            maybeInt.IfElse(value => ++counterIf, () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(0, counterElse);

            var emptyMaybeInt = Maybe<int>.None;
            emptyMaybeInt.IfElse(value => ++counterIf, () => ++counterElse);
            Assert.AreEqual(1, counterIf);
            Assert.AreEqual(1, counterElse);
        }

        [Test]
        public void MaybeOr()
        {
            Maybe<int> maybeResult;

            // Maybe with value
            var maybeInt = Maybe<int>.Some(12);
            Assert.AreEqual(12, maybeInt.Or(25));
            Assert.AreEqual(12, maybeInt.Or(() => 25));
            maybeResult = maybeInt.Or(() => 25);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(12, maybeResult.Value);
            maybeResult = maybeInt.Or(() => Maybe.None);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(12, maybeResult.Value);
            maybeResult = maybeInt.Or(() => null);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(12, maybeResult.Value);

            // Empty maybe
            Maybe<int> emptyMaybeInt = Maybe.None;
            Assert.AreEqual(42, emptyMaybeInt.Or(42));
            Assert.AreEqual(42, emptyMaybeInt.Or(() => 42));
            maybeResult = emptyMaybeInt.Or(() => 42);
            Assert.IsTrue(maybeResult.HasValue);
            Assert.AreEqual(42, maybeResult.Value);
            maybeResult = emptyMaybeInt.Or(() => Maybe.None);
            Assert.IsFalse(maybeResult.HasValue);
            maybeResult = emptyMaybeInt.Or(() => null);
            Assert.IsFalse(maybeResult.HasValue);


            Maybe<TestClass> maybeResultClass;

            // Maybe class with value
            var testObject = new TestClass();
            var defaultTestObject = new TestClass { TestInt = 12 };
            var maybeClass = Maybe<TestClass>.Some(testObject);
            Assert.AreSame(testObject, maybeClass.Or(defaultTestObject));
            Assert.AreSame(testObject, maybeClass.Or(() => defaultTestObject));
            maybeResultClass = maybeClass.Or(() => defaultTestObject);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(testObject, maybeResultClass.Value);
            maybeResultClass = maybeClass.Or(() => Maybe.None);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(testObject, maybeResultClass.Value);
            maybeResultClass = maybeClass.Or(() => null);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(testObject, maybeResultClass.Value);

            // Empty maybe class
            Maybe<TestClass> emptyMaybeClass = Maybe.None;
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Or(defaultTestObject));
            Assert.AreSame(defaultTestObject, emptyMaybeClass.Or(() => defaultTestObject));
            maybeResultClass = emptyMaybeClass.Or(() => defaultTestObject);
            Assert.IsTrue(maybeResultClass.HasValue);
            Assert.AreSame(defaultTestObject, maybeResultClass.Value);
            maybeResultClass = emptyMaybeClass.Or(() => Maybe.None);
            Assert.IsFalse(maybeResult.HasValue);
            maybeResultClass = emptyMaybeClass.Or(() => null);
            Assert.IsFalse(maybeResultClass.HasValue);
        }
    }
}