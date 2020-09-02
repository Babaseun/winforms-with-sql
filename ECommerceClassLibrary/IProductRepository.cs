using System;
using System.Collections.Generic;

namespace ECommerceClassLibrary
{
    /// <summary>
    /// An interface implementation for the product
    /// repository class
    /// </summary>
    public interface IProductRepository
    {
        List<Product> GetProducts(int paginate);
        void PostProduct(Product product);
        void DeleteProduct(Guid productID);
        void UpdateProduct(Product product);

    }
}
