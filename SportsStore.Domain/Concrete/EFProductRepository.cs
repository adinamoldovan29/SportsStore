using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{
    public class EFProductRepository : IProductsRepository
    {
        private EFDbContext dbContext = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get
            {
                return dbContext.Products;
            }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                dbContext.Products.Add(product);
            }
            else
            {
                var prod = dbContext.Products.Find(product.ProductID);
                if (prod != null)
                {
                    prod.Name = product.Name;
                    prod.Description = product.Description;
                    prod.Price = product.Price;
                    prod.Category = product.Category;
                }
            }
            dbContext.SaveChanges();
        }
    }
}
