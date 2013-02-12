using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata
{
    public class Catalogue
    {
        private readonly Dictionary<char, int> _priceList;

        public Catalogue(Dictionary<char, int> priceList)
        {
            _priceList = priceList;
        }

        public int BasePriceFor(string skus)
        {
            return skus.Sum(sku => BasePriceFor((char) sku));
        }

        private int BasePriceFor(char sku)
        {
            return _priceList[sku];
        }
    }
}