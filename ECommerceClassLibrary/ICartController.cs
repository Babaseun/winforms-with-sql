using System;
using System.Collections.Generic;

namespace ECommerceClassLibrary
{
    /// <summary>
    /// An interface implementation of the 
    /// cart controller
    /// </summary>
    public interface ICartController
    {
        void AddToCart(Cart cart);
        void DeleteFromCart(Guid ID);
        List<Product> GetProduct(string name, int price);
    }
}
