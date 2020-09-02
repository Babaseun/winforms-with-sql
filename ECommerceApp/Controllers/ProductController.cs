using ECommerceClassLibrary;
using System;
using System.Collections.Generic;

namespace E_CommerceApp
{
    public class ProductController : IProductController
    {
        IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates Product a new product and
        /// calls the database method to add new product
        /// </summary>
        /// <param name="product"></param>
        public void CreateProduct(Product product)
        {

            _repository.PostProduct(product);

        }

        /// <summary>
        /// Deletes from the database by productId
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public void DeleteProduct(Guid productID)
        {
            _repository.DeleteProduct(productID);
        }

        public void UpdateProduct(Product product)
        {

            _repository.UpdateProduct(product);

        }


        public List<Product> GetProducts(int paginate)
        {


            var output = _repository.GetProducts(paginate);

            return output;

        }

    }
}
