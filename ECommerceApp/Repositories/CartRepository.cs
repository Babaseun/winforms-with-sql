using ECommerceClassLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace E_CommerceApp
{
    class CartRepository : ICartRepository
    {
        /// <summary>
        /// A type db provider
        /// 
        /// </summary>
        readonly DbProviderFactory factory;
        string provider;
        readonly string connectionString;

        public CartRepository()
        {
            /// Gets the provider in the App.Config File
            provider = ConfigurationManager.AppSettings["provider"];
            ///Gets the connection string in  the App.Config File
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }
        /// <summary>
        /// Adds new object cart to the database
        /// 
        /// </summary>
        /// <param name="cart">Cart object</param>
        public void AddToCart(Cart cart)
        {
            try
            {

                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    var command = factory.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Cart(ID,ProductID,Quantity,CreatedAt)VALUES(default,'{cart.ProductID}','{cart.Quantity}'," +
                        $"'{cart.CreatedAt}')";
                    command.ExecuteNonQuery();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Gets a list of products 
        /// added to the cart table
        /// </summary>
        /// <returns>A list</returns>
        public List<CheckOutCart> GetCart()
        {
            var cart = new List<CheckOutCart>();
            try
            {
                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    var command = factory.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT Cart.ID ,Products.ProductName,Cart.Quantity,Products.CostPrice,Cart.CreatedAt FROM Cart INNER JOIN Products ON Cart.ProductID = Products.ProductID";
                    command.ExecuteNonQuery();

                    using (DbDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            cart.Add(new CheckOutCart
                            {
                                ID = (Guid)reader["ID"],
                                ProductName = (string)reader["ProductName"],
                                CostPrice = (int)reader["CostPrice"],
                                CreatedAt = (int)reader["CreatedAt"],
                                Quantity = (int)reader["Quantity"]

                            });

                        }
                    }
                }
                return cart;
            }



            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Deletes from the cart table by ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteFromCart(Guid ID)
        {
            try
            {
                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    var command = factory.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = $"DELETE FROM Cart WHERE ID ='{ID}'";
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Gets a list of searched product
        /// by name and price
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public List<Product> GetProductByNameAndPrice(string name, int price)
        {
            try
            {
                var products = new List<Product>();
                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    var command = factory.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT * FROM  Products WHERE ProductName = '{name}' OR CostPrice = {price}";

                    using (DbDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            products.Add(new Product
                            {
                                ProductID = (Guid)reader["ProductID"],
                                ProductName = (string)reader["ProductName"],
                                CostPrice = (int)reader["CostPrice"],
                                CreatedAt = (int)reader["CreatedAt"],
                                ModifiedAt = (int)reader["ModifiedAt"]

                            });

                        }
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
