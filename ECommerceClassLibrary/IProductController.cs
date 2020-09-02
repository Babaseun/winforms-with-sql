using System;
using System.Collections.Generic;

namespace ECommerceClassLibrary
{
    /// <summary>
    /// An interface implementation for the product controller class
    /// </summary>
    public interface IProductController
    {
        void CreateProduct(Product product);
        void DeleteProduct(Guid productID);
        void UpdateProduct(Product product);
        List<Product> GetProducts(int paginate);
    }
}
