using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.UnitTests.Helpers
{
    public static class RepositoryHelper
    {
        public static Mock<IProductsRepository> CreateProductsRepoMock()
        {
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>(){
                new Product { ProductID = 3,  Name = "P3"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 1, Name = "P1"}
            });

            return mock;
        }

        public static Mock<IProductsRepository> CreateProductsWithCategoryRepoMock()
        {
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>(){
                new Product { ProductID = 5,  Name = "P3", Category = "cat1"},
                new Product { ProductID = 4, Name = "P2", Category = "cat2"},
                new Product { ProductID = 3, Name = "P2"},
                new Product { ProductID = 2, Name = "P2", Category = "cat2"},
                new Product { ProductID = 1, Name = "P1", Category = "cat1"},
                new Product { ProductID = 1, Name = "P1", Category = ""}
            });

            return mock;
        }
    }
}
