using CustomerLogBookBL;

namespace Client
{
     public class Program
    {
        static void Main(string[] args)
        {
            CustomerServices getServices = new CustomerServices();

            var users = getServices.GetAllUser();

            foreach (var item in users)
            {
                Console.WriteLine("Customer name: " + item.name);
                Console.WriteLine("Address: " + item.address);
                Console.WriteLine("Contact Number: " + item.contactnumber);
            }
        }
    }
}
