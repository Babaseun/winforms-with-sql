using ECommerceClassLibrary;
using System;
using System.Windows.Forms;

namespace E_CommerceApp
{

    public partial class ProductUI : Form
    {

        public int Pagination = 0; // Gets a list based on the pagination value


        public ProductUI()
        {
            InitializeComponent();
        }

        private void ProductUI_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// A function for getting all
        /// the products 
        /// </summary>
        private void GetProducts()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();  /// Clears the listbox
            listBox4.Items.Clear();
            listBox5.Items.Clear();
           


            ProductRepository repository = new ProductRepository();// An object based on the blueprint ProductRepository which contains 
                                                                      // methods for database manipulation
            ProductController productt = new ProductController(repository); // 
            var products = productt.GetProducts(Pagination);
             /// Looping through each product and adding it to the list box
            products.ForEach(product =>
            {
                listBox1.Items.Add(product.ProductName);
                listBox2.Items.Add(product.CostPrice.ToString());
                listBox3.Items.Add(product.CreatedAt.ToString());
                listBox4.Items.Add(product.ProductID.ToString());
                listBox5.Items.Add(product.ModifiedAt.ToString());


            });
        }
        /// <summary>
        /// Gets all the products when the button is 
        /// clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetAllProductsButton(object sender, EventArgs e)
        {
            GetProducts();
        }
        /// <summary>
        /// Selecting the list when the list is
        /// clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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



                       /// Appending the values of the listbox to 
                       /// the text box

                ProductNameTextBox.Text = listBox1.SelectedItem.ToString();
                costPriceTextBox.Text = listBox2.SelectedItem.ToString();
                IDtextBox.Text = listBox4.SelectedItem.ToString();

            }

        }
        /// <summary>
        /// Adds product to the database by clicking the button
        /// These method collect's all the required fields needed to be 
        /// saved to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProductButton(object sender, EventArgs e)
        {
            try
            {

                ProductRepository repository = new ProductRepository();// An object of the product repository class which contains database methods


                ProductController product = new ProductController(repository); // 

                DateTime timeStamp = DateTime.UtcNow;
                long unixTime = ((DateTimeOffset)timeStamp).ToUnixTimeSeconds();

                var productData = new Product();
                productData.CostPrice = int.Parse(costPriceTextBox.Text);
                productData.CreatedAt = unixTime;
                productData.ModifiedAt = unixTime;
                productData.ProductName = ProductNameTextBox.Text;
                product.CreateProduct(productData);
                MessageBox.Show("Added product");
                GetProducts();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProductButton(object sender, EventArgs e)
        {
            try
            {
                ProductRepository repository = new ProductRepository();
                ProductController product = new ProductController(repository);
                product.DeleteProduct(Guid.Parse(IDtextBox.Text));
                MessageBox.Show("Product Deleted Successfully");
                GetProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

        }
        /// <summary>
        /// Updates product and gets the required
        /// values for updating the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButtonClicked(object sender, EventArgs e)
        {
            try
            {
                ProductRepository repository = new ProductRepository();
                ProductController product = new ProductController(repository);

                DateTime timeStamp = DateTime.UtcNow;
                long unixTime = ((DateTimeOffset)timeStamp).ToUnixTimeSeconds();

                var productData = new Product();
                productData.ProductID = Guid.Parse(IDtextBox.Text);
                productData.CostPrice = int.Parse(costPriceTextBox.Text);
                productData.ModifiedAt = unixTime;
                productData.ProductName = ProductNameTextBox.Text;
                product.UpdateProduct(productData);

                MessageBox.Show("Product Updated Successfully");
                GetProducts();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// Gets the next list of 5
        /// products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButtonClicked(object sender, EventArgs e)
        {
            try
            {
                Pagination += 5;
                GetProducts();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// Gets the previous list of 
        /// 5 products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void PreviousButtonClicked(object sender, EventArgs e)
        {


            try
            {
                Pagination -= 5;
                GetProducts();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }
    }
}
