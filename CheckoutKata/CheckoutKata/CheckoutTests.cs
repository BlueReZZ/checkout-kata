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
        [TestCase("BB", 45)]
        [TestCase("C", 20)]
        public void PriceForListOfSkusIsAsExpected(string skus, int expected)
        {
            Assert.That(_checkout.PriceFor(skus), Is.EqualTo(expected));
        }
    }

    public class Checkout
    {
        private readonly Dictionary<char, int> _prices;
        private readonly Dictionary<char, int> _counts;
        private readonly List<Offer> _offers;
        private int _total;

        public Checkout()
        {
            _prices = new Dictionary<char, int>{{'A', 50}, {'B', 30}, {'C', 20}};
            _counts = new Dictionary<char, int>();
            _offers = new List<Offer>()
                {
                    new Offer
                        {
                            SKU = 'A',
                            Frequency = 3,
                            Discount = 20
                        },

                    new Offer
                        {
                            SKU = 'B',
                            Frequency = 2,
                            Discount = 15
                        }
                };

            _total = 0;
        }

        public int PriceFor(string skus)
        {
            if (string.IsNullOrEmpty(skus))
                return _total;
            
            CalculateBasePrice(skus);
            ApplyOffers();

            return _total;
        }

        private void ApplyOffers()
        {
            foreach (var offer in _offers)
            {
                if (_counts.ContainsKey(offer.SKU))
                    _total -= offer.Discount*(_counts[offer.SKU]/offer.Frequency);
            }
        }

        private void CalculateBasePrice(string skus)
        {
            foreach (var sku in skus)
            {
                if (_counts.ContainsKey(sku))
                    _counts[sku]++;
                else
                    _counts[sku] = 1;

                _total += _prices[sku];
            }
        }
    }

    public class Offer
    {
        public char SKU { get; set; }
        public int Frequency { get; set; }
        public int Discount { get; set; }
    }
}