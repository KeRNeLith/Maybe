﻿using System;
using NUnit.Framework;
using Here.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Here.Tests.ValueObjects
{
    /// <summary>
    /// Tests for <see cref="ValueObject"/>.
    /// </summary>
    [TestFixture]
    internal class ValueObjectTests : HereTestsBase
    {
        #region Test classes

        private class Address : ValueObject
        {
            public int? Number { get; }

            public string Street { get; }

            public string City { get; }

            public Address(int? number, string street, string city)
            {
                Number = number;
                Street = street;
                City = city;
            }

            protected override IEnumerable<object> GetEqualityElements()
            {
                // Compare these fields
                yield return Number;
                yield return Street;
                yield return City;
            }
        }

        private class FullAddress : Address
        {
            public string Country { get; }

            public FullAddress(int? number, string street, string city, string country)
                : base(number, street, city)
            {
                Country = country;
            }

            protected override IEnumerable<object> GetEqualityElements()
            {
                // For derived classes
                foreach (object element in base.GetEqualityElements())
                    yield return element;

                yield return Country;
            }
        }

        private class Wallet : ValueObject
        {
            public string Currency { get; }

            public double Amount { get; }

            public Wallet(string currency, double amount)
            {
                Currency = currency;
                Amount = amount;
            }
            
            protected override IEnumerable<object> GetEqualityElements()
            {
                // To compare data stored in different formats
                yield return Currency.ToLower();
                yield return Math.Round(Amount);
            }
        }

        private class Coin : ValueObject
        {
            public enum Value
            {
                OneCent,
                TwoCents,
                FiveCents,
                TenCents,
                TwentyCents,
                FiftyCents,
                OneEuro,
                TwoEuros
            };
    
            public Value CoinValue { get; }

            public Coin(Value value)
            {
                CoinValue = value;
            }

            protected override IEnumerable<object> GetEqualityElements()
            {
                yield return CoinValue;
            }
        }

        private class CoinWallet : ValueObject
        {
            public List<Coin> Coins { get; }

            public CoinWallet([NotNull] IEnumerable<Coin> coins)
            {
                Coins = coins.ToList();
            }

            protected override IEnumerable<object> GetEqualityElements()
            {
                // To compare lists
                foreach (Coin coin in Coins)
                    yield return coin;
            }
        }

        private class SumObject : ValueObject
        {
            public int Operand1 { get; }

            public int Operand2 { get; }

            public int Sum { get; }

            public SumObject(int operand1, int operand2)
            {
                Operand1 = operand1;
                Operand2 = operand2;
                Sum = Operand1 + Operand2;
            }

            protected override IEnumerable<object> GetEqualityElements()
            {
                // Ignore "Sum" field
                yield return Operand1;
                yield return Operand2;
            }
        }

        #endregion

        #region Helpers

        [Pure]
        private void CheckAreEqual<T, T2>(T object1, T2 object2)
            where T : ValueObject
            where T2 : ValueObject
        {
            Assert.AreEqual(object1, object2);
            Assert.AreEqual(object2, object1);
            Assert.AreEqual(object1.GetHashCode(), object2.GetHashCode());

            Assert.IsTrue(object1 == object2);
            Assert.IsTrue(object2 == object1);
            Assert.IsFalse(object1 != object2);
            Assert.IsFalse(object2 != object1);
        }

        [Pure]
        private void CheckAreNotEqual<T, T2>(T object1, T2 object2)
            where T : ValueObject
            where T2 : ValueObject
        {
            Assert.AreNotEqual(object1, object2);
            Assert.AreNotEqual(object2, object1);
            Assert.AreNotEqual(object1.GetHashCode(), object2.GetHashCode());

            Assert.IsFalse(object1 == object2);
            Assert.IsFalse(object2 == object1);
            Assert.IsTrue(object1 != object2);
            Assert.IsTrue(object2 != object1);
        }

        #endregion

        [Test]
        public void BasicValueObjects()
        {
            var address1 = new Address(1, "Pila", "Montpellier");
            var address2 = new Address(1, "Pila", "Montpellier");
            CheckAreEqual(address1, address1);
        }

        [Test]
        public void ValueObjectsEdgeCases()
        {
            var address1 = new Address(1, "Pila", "Montpellier");
            Address address2 = null;
            Address address3 = null;
            
            Assert.IsFalse(address1 == address2);
            Assert.IsFalse(address2 == address1);
            Assert.IsTrue(address1 != address2);
            Assert.IsTrue(address2 != address1);

            Assert.IsTrue(address2 == address3);
            Assert.IsTrue(address3 == address2);
            Assert.IsFalse(address2 != address3);
            Assert.IsFalse(address3 != address2);
        }

        [Test]
        public void ValueObjectAndDerivedValueObject()
        {
            var address1 = new Address(1, "Pila", "Montpellier");
            var address2 = new FullAddress(1, "Pila", "Montpellier", "France");
            CheckAreNotEqual(address1, address2);
        }

        [Test]
        public void ValueObjectCustomEqualityComparison()
        {
            var wallet1 = new Wallet("EUR", 100.25);
            var wallet2 = new Wallet("Eur", 100.251111);
            CheckAreEqual(wallet1, wallet2);
        }

        [Test]
        public void ValueObjectWithCollectionField()
        {
            var wallet1 = new CoinWallet(new []{ new Coin(Coin.Value.TwoEuros), new Coin(Coin.Value.TenCents), new Coin(Coin.Value.FiftyCents) });
            var wallet2 = new CoinWallet(new []{ new Coin(Coin.Value.TwoEuros), new Coin(Coin.Value.TenCents), new Coin(Coin.Value.FiftyCents) });
            CheckAreEqual(wallet1, wallet2);
        }

        [Test]
        public void ValueObjectIgnorableField()
        {
            var sumObject1 = new SumObject(1, 2);
            var sumObject2 = new SumObject(1, 2);
            CheckAreEqual(sumObject1, sumObject2);
        }

        [Test]
        public void ValueObjectNotMatchingTypes()
        {
            var address = new Address(1, "Pila", "Montpellier");
            var wallet = new Wallet("EUR", 100.25);
            CheckAreNotEqual(address, wallet);
        }
    }
}