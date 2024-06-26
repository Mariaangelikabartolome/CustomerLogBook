using System.Net.Http.Headers;

using CustomerLogBook;

namespace CustomerLogBookDL
{
    public class UserF
    {
        public List<Model> dummyCustomer = new List<Model>();
        
        public List<Model> GetDummyCustomer()
        {
            dummyCustomer.Add(CreateDummyCustomer("Maria", "Binan City", "09274767557"));
            dummyCustomer.Add(CreateDummyCustomer("Angelika", "Sta.Rosa City", "0912345678"));

            return dummyCustomer;
        }
       public Model CreateDummyCustomer(string name, string address, string contactnumber)
        {
            Model user = new Model
            {
                name = name,
                address = address,
                contactnumber = contactnumber,

            };

            return user;
        }
    }
}
