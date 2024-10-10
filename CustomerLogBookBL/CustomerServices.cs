using CustomerLogBook;
using CustomerLogBookDL;

namespace CustomerLogBookBL
{
    public class CustomerServices
    {
        public List<Model> GetAllUser()
        {
            UserData userData = new UserData();

            return userData.GetUsers();
        }
        public Model GetUser(string name)
        {
            Model foundCustomer = new Model();

            foreach (var user in GetAllUser())
            {
                if (user.name == name)
                {
                    foundCustomer = user;
                }
            }
            return foundCustomer;
        }
        public Model GetUser(string name, string address, string contactnumber, string orders)
        {
           Model foundCustomer = new Model();

            foreach (var user in GetAllUser())
            {
                if (user.name == name && user.address == address && user.contactnumber == contactnumber && user.orders== orders)
                {
                    foundCustomer = user;
                }
            }
            return foundCustomer;

        }
    }
}

