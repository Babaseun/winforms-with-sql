using System;

namespace ECommerceClassLibrary
{
    /// <summary>
    /// A blue print of what a cart should represent
    /// </summary>
    public class Cart
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public long CreatedAt { get; set; }
    }
}
