using E_CommerceApp.Controllers;
using ECommerceClassLibrary;
using System;
using System.Windows.Forms;

namespace E_CommerceApp
{
    public partial class CartUI : Form
    {

        public int Pagination = 0;
        public CartUI()
        {
            InitializeComponent();


        }
        private void GetProducts(int paginate = 0)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();



            ProductRepository repository = new ProductRepository();


            var products = repository.GetProducts(Pagination);

            products.ForEach(product =>
            {
                listBox1.Items.Add(product.ProductName);
                listBox2.Items.Add(product.CostPrice.ToString());
                listBox3.Items.Add(product.ProductID);

            });
        }
        private void CartUI_Load(object sender, EventArgs e)
        {
            GetProducts();


        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            if (list.SelectedIndex != -1)
            {
                listBox1.SelectedIndex = list.SelectedIndex;
                listBox2.SelectedIndex = list.SelectedIndex;
                listBox3.SelectedIndex = list.SelectedIndex;


            }
        }

        private void AddToCartButton(object sender, EventArgs e)

        {
            CartRepository repository = new CartRepository();

            DateTime timeStamp = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)timeStamp).ToUnixTimeSeconds();


            try
            {
                var cart = new Cart();
                cart.ProductID = Guid.Parse(listBox3.SelectedItem.ToString());
                cart.CreatedAt = unixTime;
                cart.Quantity = int.Parse(textBox1.Text);

                CartController cartController = new CartController(repository);
                cartController.AddToCart(cart);
                MessageBox.Show("Product successfully added to cart");
            }
            catch (Exception)
            {

                MessageBox.Show("Please quantity TextBox cannot be blank");
            }
        }





        private void NextButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Pagination += 5;
                GetProducts(Pagination);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

        private void PreviousButtonClicked(object sender, EventArgs e)
        {

            try
            {
                Pagination -= 5;
                GetProducts(Pagination);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }



        }

        private void GoToCheckOutPage(object sender, EventArgs e)
        {

            this.Hide();
            CheckOutUI checkOut = new CheckOutUI();
            checkOut.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CheckOutUI checkOut = new CheckOutUI();
            checkOut.Show();

        }

        private void AdminButtonClicked(object sender, EventArgs e)
        {
            this.Hide();
            ProductUI checkOut = new ProductUI();
            checkOut.Show();
        }

        private void SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                CartRepository repository = new CartRepository();
                CartController controller = new CartController(repository);

                var products = controller.GetProduct(textBox2.Text, int.Parse(textBox3.Text));

                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();

                products.ForEach(product =>
                {
                    listBox1.Items.Add(product.ProductName);
                    listBox2.Items.Add(product.CostPrice.ToString());
                    listBox3.Items.Add(product.ProductID);

                });

            }
            catch (Exception)
            {

                MessageBox.Show("Please provide the product name and the price range to be searched for");
            }
        }
    }
}
