using Autofac.Extras.Moq;
using E_CommerceApp;
using E_CommerceApp.Controllers;
using ECommerceClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Mocking database actions
        /// and calling the ProductController class
        /// and also calling the get products
        /// of the controller class
        /// to get a list of products
        /// </summary>
       [TestMethod]
        public void GetProducts()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<IProductRepository>()
                    .Setup(x => x.GetProducts(0)).Returns(ListOfProducts());

                var cls = mock.Create<ProductController>();
                var expected = ListOfProducts();
                var actual = cls.GetProducts(0);

                Assert.IsTrue(actual != null);
                Assert.AreEqual(expected.Count, actual.Count);


                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].ProductName, actual[i].ProductName);
                    Assert.AreEqual(expected[i].CostPrice, actual[i].CostPrice);
                }
            }

        }
        /// <summary>
        /// A mock list of products
        /// </summary>
        /// <returns></returns>
        public List<Product> ListOfProducts()
        {
            var products = new List<Product>();
            products.Add(new Product
            {
                ProductID = Guid.NewGuid(),
                ProductName = "cake",
                CreatedAt = 1234567,
                CostPrice = 1000,
                ModifiedAt = 123456

            });

            products.Add(new Product
            {
                ProductID = Guid.NewGuid(),
                ProductName = "shoe",
                CreatedAt = 1734567,
                CostPrice = 1000,
                ModifiedAt = 123456

            });
            return products;
        }
        /// <summary>
        /// Mocking a database save action
        /// and unit testing to make sure
        /// the post method is called exactly 
        /// once
        /// </summary>
        [TestMethod]
        public void SaveProduct()
        {
            using (var mock = AutoMock.GetLoose())
            {
                DateTime timeStamp = DateTime.UtcNow;
                long unixTime = ((DateTimeOffset)timeStamp).ToUnixTimeSeconds();
                var product = new Product();

                product.CostPrice = 3000;
                product.CreatedAt = unixTime;
                product.ModifiedAt = unixTime;
                product.ProductName = "yam";

                mock.Mock<IProductRepository>()
                    .Setup(x => x.PostProduct(product));

                var cls = mock.Create<ProductController>();
                cls.CreateProduct(product);

                mock.Mock<IProductRepository>()
                    .Verify(x => x.PostProduct(product), Times.Exactly(1));

            }

        }
        /// <summary>
        /// Mocking an update action
        /// and unit testing The ProductController.Update
        /// product and making sure that method
        /// is called exactly once
        /// </summary>
        [TestMethod]
        public void UpdateProduct()
        {
            using (var mock = AutoMock.GetLoose())
            {
                DateTime timeStamp = DateTime.UtcNow;
                long unixTime = ((DateTimeOffset)timeStamp).ToUnixTimeSeconds();
                var product = new Product();

                product.CostPrice = 1000;
                product.ModifiedAt = unixTime;
                product.ProductName = "fish";

                mock.Mock<IProductRepository>()
                    .Setup(x => x.UpdateProduct(product));

                var cls = mock.Create<ProductController>();
                cls.UpdateProduct(product);

                mock.Mock<IProductRepository>()
                    .Verify(x => x.UpdateProduct(product), Times.Exactly(1));

            }

        }
        [TestMethod]
        public void GetASingleProduct()
        {
            using (var mock = AutoMock.GetStrict())
            {
                mock.Mock<ICartRepository>()
                    .Setup(x => x.GetProductByNameAndPrice("school bag",100)).Returns(GetProductAddedToCart());

                var cls = mock.Create<CartController>();
                var expected = GetProductAddedToCart();
                var actual = cls.GetProduct("school bag",100);

                Assert.IsTrue(actual != null);
                Assert.AreEqual(1, actual.Count);


                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i].ProductName, actual[i].ProductName);
                    Assert.AreEqual(expected[i].CostPrice, actual[i].CostPrice);
                }
            }

        }
        public List<Product> GetProductAddedToCart()
        {
            var product = new List<Product>();
            product.Add(new Product
            {
                ProductID = Guid.NewGuid(),
                ProductName = "school",
                CreatedAt = 1234567,
                CostPrice = 1000,
                ModifiedAt = 123456

            });
            return product;

        }

    }
}




























