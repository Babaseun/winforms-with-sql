using E_CommerceApp.Controllers;
using System;
using System.Windows.Forms;

namespace E_CommerceApp
{
    public partial class CheckOutUI : Form
    {
        public Guid ProductID;

        public CheckOutUI()
        {
            InitializeComponent();
        }
        private void GetListOfOrders()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            CartRepository repository = new CartRepository();

            var orders = repository.GetCart();

            orders.ForEach(product =>
            {
                listBox1.Items.Add(product.ID);
                listBox2.Items.Add(product.ProductName);
                listBox3.Items.Add(product.Quantity.ToString());
                listBox4.Items.Add(product.CostPrice.ToString());
                listBox5.Items.Add(product.CreatedAt.ToString());



            });
        }
        private void SelectedIndexChanged(object sender, EventArgs e)
        {

            ListBox list = sender as ListBox;
            if (list.SelectedIndex != -1)
            {
                listBox1.SelectedIndex = list.SelectedIndex;
                listBox2.SelectedIndex = list.SelectedIndex;
                listBox3.SelectedIndex = list.SelectedIndex;
                listBox4.SelectedIndex = list.SelectedIndex;
                listBox5.SelectedIndex = list.SelectedIndex;




                ProductID = Guid.Parse(listBox1.SelectedItem.ToString());



            }

        }

        private void CheckOutUI_Load(object sender, EventArgs e)
        {
            GetListOfOrders();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CartUI cartUI = new CartUI();
            cartUI.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CartRepository repository = new CartRepository();
            CartController cartController = new CartController(repository);
            cartController.DeleteFromCart(ProductID);


            MessageBox.Show("Deleted Successfully");
            GetListOfOrders();



        }
    }
}
