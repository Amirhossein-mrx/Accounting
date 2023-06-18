using Accounting.DataLayer.Repositories;
using Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Servies
{
    public class CustomerRepository : ICustomerRepository
    {
        private Accounting_DBEntities db;
        public CustomerRepository(Accounting_DBEntities context)
        {
            db = context;
        }


        public bool DeleteCusomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return true;
            }
        }

        public bool DeleteCusomer(int customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCusomer(customer);
                return true;
            }
            catch (Exception)
            {

                return true;
            }
        }

        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public IEnumerable<Customers> GetCustomerByFilter(string parameter)
        {
            return db.Customers.Where(c=>c.FullName.Contains(parameter)||
            c.Email.Contains(parameter)||
            c.Mobile.Contains(parameter)).ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public string GetCustomerNameById(int customerId)
        {
            return db.Customers.Find(customerId).FullName;
        }

        public List<ListCustomerViewModel> GetNameCustomers(string filter = "")
        {
            if (filter == "")
            {
                return db.Customers.Select(c => new ListCustomerViewModel()
                {
                    CustomerId = c.CustomerID,
                    FullName = c.FullName


                }).ToList();
            }
            return db.Customers.Where(c => c.FullName.Contains(filter)).Select(c =>  new ListCustomerViewModel()
            {
                CustomerId = c.CustomerID,
                FullName = c.FullName


            }).ToList();
        }

        public bool InsertCusomer(Customers customer)
        {
            try
            {
                 db.Customers.Add(customer);
                return true;
            }
            catch (Exception)
            {

                return true;
            }
            
        }

        

        public bool UpdateCusomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return true;
            }
        }
    }
}
