using NUnit.Framework;

namespace CheckoutKata
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void EmptyCheckoutHasZeroPrice()
        {
            Assert.That(new Checkout().PriceFor(""), Is.EqualTo(0));
        }

        [Test]
        [TestCase("A", 50)]
        public void PriceForListOfSkusIsAsExpected(string skus, int expected)
        {
            Assert.That(new Checkout().PriceFor(skus), Is.EqualTo(expected));
        }
    }

    public class Checkout
    {
        public int PriceFor(string skus)
        {
            if (string.IsNullOrEmpty(skus))
                return 0;

            return 50;
        }
    }
}