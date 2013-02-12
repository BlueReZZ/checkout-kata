using System.Collections.Generic;

namespace CheckoutKata
{
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
}