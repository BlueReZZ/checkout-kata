using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Offers
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
}