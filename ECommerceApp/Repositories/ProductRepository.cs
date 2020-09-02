using ECommerceClassLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;

namespace E_CommerceApp
{
    public class ProductRepository : IProductRepository
    {
        readonly DbProviderFactory factory;
        string provider;
        readonly string connectionString;
        public ProductRepository()
        {
           /// Connects to the database via the Appsettings Provider
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"]; // Gets the connection string from the  App.config file
            factory = DbProviderFactories.GetFactory(provider);

        }
        /// <summary>
        /// Gets a list of products
        /// </summary>
        /// <param name="paginate"></param>
        /// <returns>products</returns>
        public List<Product> GetProducts(int paginate = 0)
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
                    command.CommandText = $"SELECT* FROM Products ORDER BY ModifiedAt DESC OFFSET {paginate} ROWS FETCH NEXT 5 ROWS ONLY";

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
                throw ex;
            }
        }
        /// <summary>
        /// Adds product to the database
        /// </summary>
        /// <param name="product"></param>
        public void PostProduct(Product product)
        {
            try
            {
                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    var command = factory.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Products(ProductID,ProductName,CostPrice,CreatedAt,ModifiedAt)VALUES(default,'{product.ProductName}','{product.CostPrice}'," +
                        $"'{product.CreatedAt}','{product.ModifiedAt}')";
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
        /// Deletes product from  the database
        /// by the product ID
        /// </summary>
        /// <param name="productID"></param>
        public void DeleteProduct(Guid productID)
        {
            try
            {
                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    var command = factory.CreateCommand();
                    command.Connection = connection;
                    command.CommandText = $"DELETE FROM Products WHERE ProductID ='{productID}'";
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
        /// Updates product
        /// </summary>
        /// <param name="product">Product to be updated</param>
        public void UpdateProduct(Product product)
        {
            try
            {
                using (var connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    var command = factory.CreateCommand();
                    command.Connection = connection;


                    command.CommandText = $"UPDATE Products SET ProductName ='{product.ProductName}',CostPrice='{product.CostPrice}'," +
                        $"ModifiedAt='{product.ModifiedAt} 'WHERE ProductID = '{product.ProductID}'";
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
