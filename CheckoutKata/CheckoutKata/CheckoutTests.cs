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
    }

    public class Checkout
    {
        public int PriceFor(string skus)
        {
            return 0;
        }
    }
}