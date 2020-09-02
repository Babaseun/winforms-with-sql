using System;

namespace ECommerceClassLibrary
{
    /// <summary>
    /// A blue print of what a product should 
    /// look like
    /// </summary>
    public class Product
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public int CostPrice { get; set; }
        public long ModifiedAt { get; set; }
        public long CreatedAt { get; set; }

    }
}
