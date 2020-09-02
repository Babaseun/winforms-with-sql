using ECommerceClassLibrary;
using System;
using System.Collections.Generic;

namespace E_CommerceApp.Controllers
{
    public class CartController : ICartController
    {
        ICartRepository _repository;
        /// <summary>
        /// These constructor takes an implementation 
        /// of an interface which has all the methods
        /// for performing crud operations
        /// </summary>
        /// <param name="repository"></param>
        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Adds to the cart table 
        /// by calling the database method addToCart
        /// and takes in a cart object
        /// </summary>
        /// <param name="cart"></param>
        public void AddToCart(Cart cart)
        {

            _repository.AddToCart(cart);

        }
        /// <summary>
        /// Deletes from the cart table in the database
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteFromCart(Guid ID)
        {

            _repository.DeleteFromCart(ID);

        }
        /// <summary>
        /// Returns a list of searched products
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public List<Product> GetProduct(string name,int price)
        {

           return _repository.GetProductByNameAndPrice(name,price);

        }

    }
}
