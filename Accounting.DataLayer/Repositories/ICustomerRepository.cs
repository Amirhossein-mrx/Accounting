using Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customers> GetCustomerByFilter(string parameter);
        List<Customers> GetAllCustomers();
        List<ListCustomerViewModel> GetNameCustomers(string filter="");
        Customers GetCustomerById(int customerId);
        bool InsertCusomer(Customers customer);
        bool UpdateCusomer(Customers customer);
        bool DeleteCusomer(Customers customer);
        bool DeleteCusomer(int customerId);
        string GetCustomerNameById(int customerId);
       
    }
}
