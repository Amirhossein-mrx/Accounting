using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.Ap
{
    public partial class frmCustomers : Form
    {
        public frmCustomers()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            BindGride();
        }
        void BindGride()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource=db.CustomerRepository.GetAllCustomers();
            }
                
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            BindGride();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                
                dgvCustomers.DataSource = db.CustomerRepository.GetCustomerByFilter(txtFilter.Text);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            frmAddOrEditCustomer frmAddOrEditCustomer = new frmAddOrEditCustomer();
            if(frmAddOrEditCustomer.ShowDialog()==DialogResult.OK)
            {
                BindGride();
            }
        }

        private void txtFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                    string name = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                    if (RtlMessageBox.Show($"آیا از حذف {name} مطمئین هستید؟", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)

                    {
                        db.CustomerRepository.DeleteCusomer(customerId);
                        db.Save();
                        BindGride();
                    }
                }
            }

            else
            {
                RtlMessageBox.Show("لطفا شخصی را انتخاب کنید");
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if(dgvCustomers.CurrentRow != null)
            {
                int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                    frmAddOrEditCustomer frmAddOrEditCustomer = new frmAddOrEditCustomer();
                    frmAddOrEditCustomer.customerId = customerId;
                    if(frmAddOrEditCustomer.ShowDialog()==DialogResult.OK)
                    {
                        BindGride();
                    }
                   
                }
            }
        }
    }
}
