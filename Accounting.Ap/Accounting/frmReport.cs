using Accounting.DataLayer.Context;
using Accounting.Utility.Convertor;
using Accounting.ViewModels;
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
    public partial class frmReport : Form
    {
        public int TypeId = 0;
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<ListCustomerViewModel> list = new List<ListCustomerViewModel>();
                list.Add(new ListCustomerViewModel() 
                { 
                    CustomerId=0,
                    FullName="انتخاب کنید"
                });
                list.AddRange(db.CustomerRepository.GetNameCustomers());
                cbCustomer.DataSource = list;
                cbCustomer.DisplayMember = "Fullname";
                cbCustomer.ValueMember = "CustomerID";
            }
               
            if (TypeId == 1)
            {
                this.Text = "گزارش دریافتی ها";
            }
            else
            {
                this.Text = "گزارش پرداختی ها";
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }
        void Filter()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DataLayer.Accounting> result = new List<DataLayer.Accounting>();
                DateTime? startDate;
                DateTime? endDate;

                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int customerId = int.Parse(cbCustomer.SelectedValue.ToString());
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeId && a.CustomerID == customerId));
                }
                else
                {
                    result.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeId));
                }
                 
                if (txtFromData.Text !="    /  /")
                {
                    startDate = Convert.ToDateTime(txtFromData.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    result = result.Where(r => r.DateTitle >= startDate.Value).ToList();
                }
                if (txtToData.Text != "    /  /")
                {
                    endDate = Convert.ToDateTime(txtToData.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    result = result.Where(r => r.DateTitle <= endDate.Value).ToList();
                }
                //dgReport.AutoGenerateColumns = false;
                //dgReport.DataSource = result;
                dgReport.Rows.Clear();
                foreach (var accounting in result)
                {
                    string CustomerName = db.CustomerRepository.GetCustomerNameById(accounting.CustomerID);
                    dgReport.Rows.Add(accounting.ID, CustomerName, accounting.Amount, accounting.DateTitle.ToShamsy(), accounting.Description);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int accuntingId = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                
                    if (RtlMessageBox.Show("آیا از حذف مطمئین هستید! ", "توجه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)

                    {
                    using (UnitOfWork db = new UnitOfWork())
                    {
                        db.AccountingRepository.Delete(accuntingId);
                        db.Save();
                        Filter();
                    }
                }
            }

            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dgReport.CurrentRow != null)
            {
                int accuntingId = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                using (UnitOfWork db = new UnitOfWork())
                {
                    frmNewAccounting frmNewAccounting = new frmNewAccounting();
                    frmNewAccounting.AccountingId = accuntingId;
                    if (frmNewAccounting.ShowDialog() == DialogResult.OK)
                    {
                        Filter();
                    }

                }
            }
        }
    }
}
