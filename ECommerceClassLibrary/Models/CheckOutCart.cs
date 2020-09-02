using System;


namespace ECommerceClassLibrary
{
    /// <summary>
    /// A Blue print of what a cart 
    /// should contain
    /// </summary>
    public class CheckOutCart
    {
        public Guid ID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int CreatedAt { get; set; }
        public int CostPrice { get; set; }
    }
}
