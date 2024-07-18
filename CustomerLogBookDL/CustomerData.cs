using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerLogBook;

namespace CustomerLogBookDL
{

        public class UserData
        {
            List<Model> users;
            SqlDBData sqlData;
            public UserData()
            {
                users = new List<Model>();
                sqlData = new SqlDBData();


            }
            public List<Model> GetUsers()
            {
                users = sqlData.GetUsers();
                return users;
            }

        public int DeleteCustomer(Model user)
            {
                return sqlData.DeleteCustomer(user.name);
            }
            public int AddCustomer(Model user)
            {
                return sqlData.AddCustomer(user.name, user.address, user.contactnumber);
            }
            public int UpdateCustomer(Model user)
            {
                return sqlData.UpdateCustomer(user.name, user.address);
            }
        }
}
