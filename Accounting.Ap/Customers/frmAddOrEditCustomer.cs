using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.Ap
{
    public partial class frmAddOrEditCustomer : Form
    {
        public int customerId=0;
        public frmAddOrEditCustomer()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pcCustomer.ImageLocation = openFile.FileName;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                //make unik name for image
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);

                //save image
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path + ImageName);


                using (UnitOfWork db = new UnitOfWork())
                {
                    Customers customer = new Customers()
                    {
                        FullName = txtName.Text,
                        Mobile = txtMobile.Text,
                        Email = txtEmail.Text,
                        Address = txtAddress.Text,
                        CustomerImage = ImageName

                    };
                    if (customerId==0)
                    {
                        
                        db.CustomerRepository.InsertCusomer(customer);
                    }
                    else
                    {
                        customer.CustomerID = customerId;
                        db.CustomerRepository.UpdateCusomer(customer);
                    }
                    
                    db.Save();
                    DialogResult = DialogResult.OK;

                }
            }
        }

        private void frmAddOrEditCustomer_Load(object sender, EventArgs e)
        {
            if(customerId!=0)
            {
               
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";
                using (UnitOfWork db = new UnitOfWork())
                {
                    Customers customer =db.CustomerRepository.GetCustomerById(customerId);
                    txtName.Text = customer.FullName;
                    txtMobile.Text = customer.Mobile;
                    txtEmail.Text = customer.Email;
                    txtAddress.Text = customer.Address;
                    pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;
                }

             }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
