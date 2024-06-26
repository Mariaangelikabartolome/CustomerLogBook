using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLogBookBL
{
    public class CustomerValidation
    {
        CustomerServices getservices = new CustomerServices();

        public bool CheckIfCustomerNameExists(string name)
        {
            bool result = getservices.GetUser(name) != null;
            return result; 

        }
        public bool CheckIfCustomerNameExists(string name, string address, string contactnumber)
        {
            bool result = getservices.GetUser(name, address, contactnumber) != null;
            return result;  
        }
    }
}
