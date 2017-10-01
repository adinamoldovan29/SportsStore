using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> cartLineCollection = new List<CartLine>();

        public IEnumerable<CartLine> CartLines
        {
            get { return cartLineCollection; }
        }

        public void AddToCart(Product product, int quantity)
        {
            var line = cartLineCollection
                .Where(cl => cl.Product.ProductID == product.ProductID)
                .SingleOrDefault();

            if (line != null)
            {
                line.Quantity += quantity;
            }
            else
            {
                cartLineCollection.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }

        }

        public decimal ComputeCartValue()
        {
            return cartLineCollection.Sum(cl => cl.Product.Price * cl.Quantity);
        }

        public void RemoveLine(Product product)
        {
            cartLineCollection.RemoveAll(cl => cl.Product.ProductID == product.ProductID);
        }

        public void ClearCart()
        {
            cartLineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
