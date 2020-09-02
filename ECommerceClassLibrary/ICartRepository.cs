using System;
using System.Collections.Generic;

namespace ECommerceClassLibrary
{
    /// <summary>
    /// An interface for the CartRepository
    /// </summary>
    public interface ICartRepository
    {
        void AddToCart(Cart cart);
        List<CheckOutCart> GetCart();
        void DeleteFromCart(Guid ID);
        List<Product> GetProductByNameAndPrice(string name, int price);

    }
}
