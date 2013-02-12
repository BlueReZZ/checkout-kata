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
            var offers = new Offers(new List<Offer>
                {
                    new Offer{SKU = 'A', Frequency = 3, Discount = 20},
                    new Offer{SKU = 'B', Frequency = 2, Discount = 15}
                });
            var catalogue = new Catalogue(new Dictionary<char, int> {{'A', 50}, {'B', 30}, {'C', 20}, {'D', 15}});
            
            _checkout = new Checkout(offers, catalogue);
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
        [TestCase("BB", 45)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        [TestCase("CDBA", 115)]
        [TestCase("AAABB", 175)]
        [TestCase("ABABA", 175)]
        public void PriceForListOfSkusIsAsExpected(string skus, int expected)
        {
            Assert.That(_checkout.PriceFor(skus), Is.EqualTo(expected));
        }
    }
}