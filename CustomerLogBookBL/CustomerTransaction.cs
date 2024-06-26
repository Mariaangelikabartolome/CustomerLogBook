using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerLogBook;

using CustomerLogBookDL;

namespace CustomerLogBookBL
{
    public class CustomerTransaction

    {
        CustomerValidation validationServices = new CustomerValidation();
        UserData userData = new UserData(); 
      

        public bool CreateCustomer(Model user)
        {
            bool result = false;

            if (validationServices.CheckIfCustomerNameExists(user.name))
            {
                result = userData.AddCustomer(user) > 0;
            }

            return result;
        }

        public bool CreateCustomer(string name, string address, string contactnumber)
        {
            Model user = new Model { name = name, address = address, contactnumber = contactnumber };

            return CreateCustomer(user);
        }

        public bool UpdateCustomer(Model user)
        {
            bool result = false;

            if (validationServices.CheckIfCustomerNameExists(user.name))
            {
                result = userData.UpdateCustomer(user) > 0;
            }

            return result;
        }

        public bool UpdateCustomer(string name, string address, string contactnumber)
        {
            Model user = new Model { name = name, address = address, contactnumber = contactnumber };

            return UpdateCustomer(user);
        }

        public bool DeleteUser(Model user)
        {
            bool result = false;

            if (validationServices.CheckIfCustomerNameExists(user.name))
            {
                result = userData.DeleteCustomer(user) > 0;
            }

            return result;
        }
    }
}

  
