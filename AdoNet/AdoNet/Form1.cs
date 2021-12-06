using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productDal = new ProductDal();
        ProductDal productDal = new ProductDal();

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();

        }

        private void LoadProducts()
        {
            dgwProducts.DataSource = productDal.GetAll();
        }



        //add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productDal.Add(new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)

            });

            MessageBox.Show("Product added! " + tbxName.Text);
            LoadProducts();
        }

       

        //select action
        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString(); //select row
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString(); 
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();




        }
        //update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
            try
            {
                Product product = new Product

                {
                    Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value.ToString()),
                    Name = tbxNameUpdate.Text,
                    UnitPrice = Convert.ToDecimal(tbxStockAmountUpdate.Text),
                    StockAmount = Convert.ToInt32(tbxUnitPriceUpdate.Text)




                };
                _productDal.Update(product);
                 MessageBox.Show("Updated! " + tbxNameUpdate.Text);
            }
            catch (Exception ex )
            {

                MessageBox.Show("Error!" + ex.Message);
            }
          
           
            LoadProducts();
                
        }
        //delete
        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value.ToString());
            string name = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            _productDal.Delete(id);

            MessageBox.Show("Deleted! " + name );
            LoadProducts();



        }

       
        //delete all records
        private void button1_Click(object sender, EventArgs e)
        {
            _productDal.DeleteAll();
            MessageBox.Show("All Fields Are Deleted! ");
            LoadProducts();
        }
    }
}
