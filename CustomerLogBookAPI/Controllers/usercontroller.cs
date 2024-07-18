using CustomerLogBookBL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using CustomerLogBookAPI;

namespace CustomerLogBookAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        CustomerServices _userGetServices;
        CustomerTransaction _userTransactionServices;

        public UserController()
        {
            _userGetServices = new CustomerServices();
            _userTransactionServices = new CustomerTransaction();
        }

        [HttpGet]
        public IEnumerable<CustomerLogBookAPI.Model> GetUsers()
        {
            var activeusers = _userGetServices.GetAllUser();

            List<CustomerLogBookAPI.Model> users = new List<Model>();

            foreach (var item in activeusers)
            {
                users.Add(new CustomerLogBookAPI.Model { name = item.name, address = item.address, contactnumber = item.contactnumber });
            }

            return users;
        }

        [HttpPost]
        public JsonResult AddUser(Model request)
        {
            var result = _userTransactionServices.CreateCustomer(request.name, request.address, request.contactnumber);

            return new JsonResult(result);
        }

        [HttpPatch]
        public JsonResult UpdateUser(Model request)
        {
            var result = _userTransactionServices.UpdateCustomer(request.name, request.address);

            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult DeleteUser(CustomerLogBookAPI.Model request)
        {

            var CustomerToDelete = new CustomerLogBook.Model
            {
                name = request.name

            };

            var result = _userTransactionServices.DeleteUser(CustomerToDelete);

            return new JsonResult(result);
        }
    }
}