using System.Collections.Generic;
using System.Linq;
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
        [TestCase("D", 15)]
        [TestCase("CDBA", 115)]
        [TestCase("AAABB", 175)]
        [TestCase("ABABA", 175)]
        public void PriceForListOfSkusIsAsExpected(string skus, int expected)
        {
            Assert.That(_checkout.PriceFor(skus), Is.EqualTo(expected));
        }
    }

    public class Checkout
    {
        private readonly Catalogue _prices;
        private readonly Offers _offers;

        public Checkout()
        {
            _prices = new Catalogue(new Dictionary<char, int>{{'A', 50}, {'B', 30}, {'C', 20}, {'D', 15}});
            new Dictionary<char, int>();
            _offers = new Offers(new List<Offer>()
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
                });
        }

        public int PriceFor(string skus)
        {
            var total = 0;
            if (string.IsNullOrEmpty(skus))
                return total;
            
            total = _prices.BasePriceFor(skus);

            total -= _offers.FindDiscountFor(skus);

            return total;
        }
    }

    public class Offers : List<Offer>
    {
        private readonly List<Offer> _offers;

        public Offers(List<Offer> offers)
        {
            _offers = offers;
            
        }

        public int FindDiscountFor(string skus)
        {
            var counts = new Dictionary<char, int>();
            foreach (var sku in skus)
            {
                if (counts.ContainsKey(sku))
                    counts[sku]++;
                else
                   counts[sku] = 1;
            }

            return FindDiscountFor(counts);
        }

        private int FindDiscountFor(Dictionary<char, int> counts)
        {
            return _offers
                .Where(offer => counts.ContainsKey(offer.SKU))
                .Sum(offer => (offer.Discount*(counts[offer.SKU]/offer.Frequency)));
        }
    }

    public class Catalogue
    {
        private readonly Dictionary<char, int> _priceList;

        public Catalogue(Dictionary<char, int> priceList)
        {
            _priceList = priceList;
        }

        public int BasePriceFor(string skus)
        {
            return skus.Sum(sku => BasePriceFor(sku));
        }

        private int BasePriceFor(char sku)
        {
            return _priceList[sku];
        }
    }

    public class Offer
    {
        public char SKU { get; set; }
        public int Frequency { get; set; }
        public int Discount { get; set; }
    }
}