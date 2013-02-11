using System.Collections.Generic;
using NUnit.Framework;

namespace CheckoutKata
{
    [TestFixture]
    public class CheckoutTests
    {
        private Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            _checkout = new Checkout();
        }

        [Test]
        public void EmptyCheckoutHasZeroPrice()
        {
            Assert.That(_checkout.PriceFor(""), Is.EqualTo(0));
        }

        [Test]
        [TestCase("A", 50)]
        [TestCase("AA", 100)]
        [TestCase("AAA", 130)]
        [TestCase("AAAAAA", 260)]
        [TestCase("B", 30)]
        public void PriceForListOfSkusIsAsExpected(string skus, int expected)
        {
            Assert.That(_checkout.PriceFor(skus), Is.EqualTo(expected));
        }
    }

    public class Checkout
    {
        public int PriceFor(string skus)
        {
            var total = 0;
            var countOfA = 0;
            var prices = new Dictionary<char, int>{{'A', 50}, {'B', 30}};
            var counts = new Dictionary<char, int>();

            if (string.IsNullOrEmpty(skus))
                return total;
            
            foreach (var sku in skus)
            {
                if (counts.ContainsKey(sku))
                    counts[sku]++;
                else
                    counts[sku] = 1;

                total += prices[sku];
            }

            if(counts.ContainsKey('A'))
                total -= 20 * (counts['A'] / 3);

            return total;
        }
    }
}