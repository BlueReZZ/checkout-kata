using System.Collections.Generic;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly Catalogue _prices;
        private readonly Offers _offers;

        public Checkout(Offers offers, Catalogue catalogue)
        {
            _prices = catalogue;
            _offers = offers;
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
}