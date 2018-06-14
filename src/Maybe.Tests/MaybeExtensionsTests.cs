﻿using NUnit.Framework;
using Here.Maybes.Extensions;
using System.Collections.Generic;
using System;

namespace Here.Maybes.Tests
{
    /// <summary>
    /// Tests for <see cref="Maybe{T}"/>.
    /// </summary>
    [TestFixture]
    internal class MaybeExtensionsTests : MaybeTestsBase
    {
        #region Test methods

        private int GetInt()
        {
            return 12;
        }

        private int? GetNullableInt()
        {
            return 42;
        }

        private int? GetNullNullableInt()
        {
            return null;
        }

        private TestClass GetTestClass()
        {
            return new TestClass();
        }

        private TestClass GetNullTestClass()
        {
            return null;
        }

        #endregion

        [Test]
        public void ToMaybe()
        {
            // Value type
            int integer = 1;
            var maybeInt = integer.ToMaybe();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(1, maybeInt.Value);

            // Nullable
            var testNullableNull = (int?)null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var maybeNullableNull = testNullableNull.ToMaybe();
            Assert.IsFalse(maybeNullableNull.HasValue);

            TestStruct? testNullableNotNull = new TestStruct();
            var maybeNullableNotNull = testNullableNotNull.ToMaybe();
            Assert.IsTrue(maybeNullableNotNull.HasValue);
            Assert.AreEqual(testNullableNotNull.Value, maybeNullableNotNull.Value);

            // Reference type
            var testObject = new TestClass();
            var maybeClass = testObject.ToMaybe();
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreSame(testObject, maybeClass.Value);

            TestClass testObjectNull = null;
            maybeClass = MaybeExtensions.ToMaybe(testObjectNull);
            Assert.IsFalse(maybeClass.HasValue);
        }

        [Test]
        public void ReturnToMaybe()
        {
            // Value type
            Maybe<int> maybeInt = GetInt();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(12, maybeInt.Value);

            // Nullable
            Maybe<int?> maybeNullableInt = GetNullableInt();
            Assert.IsTrue(maybeNullableInt.HasValue);
            Assert.AreEqual(42, maybeNullableInt.Value);

            maybeNullableInt = GetNullNullableInt();
            Assert.IsFalse(maybeNullableInt.HasValue);

            // Reference type
            Maybe<TestClass> maybeClass = GetTestClass();
            Assert.IsTrue(maybeClass.HasValue);
            Assert.AreEqual(new TestClass(), maybeClass.Value);

            maybeClass = GetNullTestClass();
            Assert.IsFalse(maybeClass.HasValue);
        }

        [Test]
        public void MaybeToNullable()
        {
            // Value type
            var maybeInt = Maybe<int>.Some(12);
            var intNullable = maybeInt.ToNullable();
            Assert.IsTrue(intNullable.HasValue);
            Assert.AreEqual(12, intNullable.Value);

            maybeInt = Maybe.None;
            intNullable = maybeInt.ToNullable();
            Assert.IsFalse(intNullable.HasValue);
        }

        [Test]
        public void StringToMaybe()
        {
            Maybe<string> maybeString;

            string testEmpty = string.Empty;
            maybeString = testEmpty.NoneIfEmpty();
            Assert.IsFalse(maybeString.HasValue);

            string testEmpty2 = "";
            maybeString = testEmpty2.NoneIfEmpty();
            Assert.IsFalse(maybeString.HasValue);

            string testNull = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            maybeString = MaybeExtensions.NoneIfEmpty(testNull);
            Assert.IsFalse(maybeString.HasValue);

            string notEmpty = "test";
            maybeString = notEmpty.NoneIfEmpty();
            Assert.IsTrue(maybeString.HasValue);
            Assert.AreEqual("test", maybeString.Value);
        }

        [Test]
        public void EnumerableFirstOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var maybeInt = enumerableInts.FirstOrNone();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(1, maybeInt.Value);
            maybeInt = enumerableInts.FirstOrNone(value => value == 2);
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(2, maybeInt.Value);

            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.FirstOrNone();
            Assert.IsFalse(maybeInt.HasValue);
            maybeInt = enumerableInts2.FirstOrNone(value => value == 2);
            Assert.IsFalse(maybeInt.HasValue);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.FirstOrNone();
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(1, maybeNullable.Value);
            maybeNullable = enumerableNullableInts.FirstOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(2, maybeNullable.Value);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.FirstOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts2.FirstOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.FirstOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts3.FirstOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeNullable2 = enumerableTestClass.FirstOrNone();
            Assert.IsTrue(maybeNullable2.HasValue);
            Assert.AreSame(testObj1, maybeNullable2.Value);
            maybeNullable2 = enumerableTestClass.FirstOrNone(value => ReferenceEquals(testObj2, value));
            Assert.IsTrue(maybeNullable2.HasValue);
            Assert.AreSame(testObj2, maybeNullable2.Value);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeNullable2 = enumerableTestClass2.FirstOrNone();
            Assert.IsFalse(maybeNullable2.HasValue);
            maybeNullable2 = enumerableTestClass2.FirstOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeNullable2.HasValue);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeNullable2 = enumerableTestClass3.FirstOrNone();
            Assert.IsFalse(maybeNullable2.HasValue);
            maybeNullable2 = enumerableTestClass3.FirstOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeNullable2.HasValue);
        }

        [Test]
        public void EnumerableLastOrNone()
        {
            // Value type
            IEnumerable<int> enumerableInts = new List<int> { 1, 2, 3 };
            var maybeInt = enumerableInts.LastOrNone();
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(3, maybeInt.Value);
            maybeInt = enumerableInts.LastOrNone(value => value == 1);
            Assert.IsTrue(maybeInt.HasValue);
            Assert.AreEqual(1, maybeInt.Value);

            IEnumerable<int> enumerableInts2 = new List<int>();
            maybeInt = enumerableInts2.LastOrNone();
            Assert.IsFalse(maybeInt.HasValue);
            maybeInt = enumerableInts2.LastOrNone(value => value == 2);
            Assert.IsFalse(maybeInt.HasValue);

            // Nullables
            IEnumerable<int?> enumerableNullableInts = new List<int?> { 1, 2, 3 };
            var maybeNullable = enumerableNullableInts.LastOrNone();
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(3, maybeNullable.Value);
            maybeNullable = enumerableNullableInts.LastOrNone(value => value.HasValue && value.Value == 1);
            Assert.IsTrue(maybeNullable.HasValue);
            Assert.AreEqual(1, maybeNullable.Value);

            IEnumerable<int?> enumerableNullableInts2 = new List<int?> { null, null };
            maybeNullable = enumerableNullableInts2.LastOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts2.LastOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            IEnumerable<int?> enumerableNullableInts3 = new List<int?>();
            maybeNullable = enumerableNullableInts3.LastOrNone();
            Assert.IsFalse(maybeNullable.HasValue);
            maybeNullable = enumerableNullableInts3.LastOrNone(value => value.HasValue && value.Value == 2);
            Assert.IsFalse(maybeNullable.HasValue);

            // Reference type
            var testObj1 = new TestClass();
            var testObj2 = new TestClass();
            IEnumerable<TestClass> enumerableTestClass = new List<TestClass> { testObj1, testObj2 };
            var maybeNullable2 = enumerableTestClass.LastOrNone();
            Assert.IsTrue(maybeNullable2.HasValue);
            Assert.AreSame(testObj2, maybeNullable2.Value);
            maybeNullable2 = enumerableTestClass.LastOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsTrue(maybeNullable2.HasValue);
            Assert.AreSame(testObj1, maybeNullable2.Value);

            IEnumerable<TestClass> enumerableTestClass2 = new List<TestClass> { null, null };
            maybeNullable2 = enumerableTestClass2.LastOrNone();
            Assert.IsFalse(maybeNullable2.HasValue);
            maybeNullable2 = enumerableTestClass2.LastOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeNullable2.HasValue);

            IEnumerable<TestClass> enumerableTestClass3 = new List<TestClass>();
            maybeNullable2 = enumerableTestClass3.LastOrNone();
            Assert.IsFalse(maybeNullable2.HasValue);
            maybeNullable2 = enumerableTestClass3.LastOrNone(value => ReferenceEquals(testObj1, value));
            Assert.IsFalse(maybeNullable2.HasValue);
        }
    }
}