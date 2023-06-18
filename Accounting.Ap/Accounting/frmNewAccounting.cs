using Accounting.DataLayer;
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
using ValidationComponents;

namespace Accounting.Ap
{
    public partial class frmNewAccounting : Form
    {
        public int AccountingId = 0;
        public frmNewAccounting()
        {
            InitializeComponent();
        }

        private void frmNewAccounting_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomer.AutoGenerateColumns = false;
                dgvCustomer.DataSource = db.CustomerRepository.GetNameCustomers();
                if (AccountingId != 0)
                {

                    this.Text = "ویرایش تراکنش";
                    DataLayer.Accounting account = db.AccountingRepository.GetById(AccountingId);
                    txtAmount.Value = account.Amount;
                    txtDesctiption.Text = account.Description;
                    txtName.Text = account.Customers.FullName;



                    if (account.TypeID == 1)
                    {
                        rbRecive.Checked = true;
                    }
                    else
                    {
                        rbPay.Checked = true;

                    }
                }
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomer.AutoGenerateColumns = false;
                dgvCustomer.DataSource = db.CustomerRepository.GetNameCustomers(txtFilter.Text);
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtName.Text = dgvCustomer.CurrentRow.Cells[0].Value.ToString();


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbRecive.Checked || rbPay.Checked)
                {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        DataLayer.Accounting accounting = new DataLayer.Accounting()
                        {
                            Amount = int.Parse(txtAmount.Value.ToString()),
                            Description = txtDesctiption.Text,
                            CustomerID = int.Parse(dgvCustomer.CurrentRow.Cells[1].Value.ToString()),
                            
                            TypeID = (rbRecive.Checked) ? 1 : 2,
                            DateTitle = DateTime.Now

                        };

                        if (AccountingId == 0)
                        {
                            db.AccountingRepository.Insert(accounting);
                        }
                        else
                        {
                            accounting.ID = AccountingId;
                            db.AccountingRepository.Update(accounting);
                        }
                        db.Save();
                        DialogResult = DialogResult.OK;
                    }

                }
                else
                {
                    RtlMessageBox.Show("لطغا نوع تراکنش را انتخاب کنید");
                }

            }

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
