using CustomerLogBookBL;
using MailKit.Net.Smtp;
using MimeKit;
using CustomerLogBook;
using System;
using System.Collections.Generic;

namespace Client
{
    public class Program
    {
        static List<Jewelry> JewelryList = new List<Jewelry>();

        static void Main(string[] args)
        {
           
            InitializeJewelryList();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nWelcome to Maria's Jewelry Shop:");
                Console.WriteLine("\nPress 1: View Jewelry List");
                Console.WriteLine("\nPress 2: Order");
                Console.WriteLine("\nPress 3: Cancel Order");
                Console.WriteLine("\nPress 4: Exit");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 to 4.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        ViewJewelryList();
                        break;
                    case 2:
                        PlaceOrder();
                        break;
                    case 3:
                        CancelOrder();
                        break;
                    case 4:
                        exit = true;
                        Console.WriteLine("Thank you for visiting Maria's Jewelry Shop!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }

        static void InitializeJewelryList()
        {
            Jewelry Necklace = new Jewelry { model = "Love Wedding Band", brand = "Cartier", description = "Yellow Gold. Width: 3.6 mm" };
            JewelryList.Add(Necklace);

            Jewelry Ring = new Jewelry { model = "Sparkling Bow Necklace", brand = "Pandora", description = "Sterling silver. size: 50" };
            JewelryList.Add(Ring);

            Jewelry Earrings = new Jewelry { model = "Tiffany Lock", brand = "Tiffany&Co", description = "White Gold with Diamonds, size: 50" };
            JewelryList.Add(Earrings);

            Jewelry Bracelet = new Jewelry { model = "Hole In One Statement Charm Bracelet", brand = "Katespade", description = "Chain length: 8 Weight: 35.95g" };
            JewelryList.Add(Bracelet);
        }

        static void ViewJewelryList()
        {
            Console.WriteLine("\nHere is the jewelry list:");
            foreach (var jewelry in JewelryList)
            {
                Console.WriteLine($"{jewelry.brand} {jewelry.model} - {jewelry.description}");
            }
        }

        static void PlaceOrder()
        {
            Console.WriteLine("\nEnter your full name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your address:");
            string address = Console.ReadLine();
            Console.WriteLine("Enter your contact number:");
            string contactNumber = Console.ReadLine();

            Console.WriteLine("Choose a jewelry by entering the model name:");
            string model = Console.ReadLine();

            Jewelry selectedJewelry = JewelryList.Find(j => j.model == model);

            if (selectedJewelry != null)
            {
                string orders = selectedJewelry.model;

                CustomerTransaction customerTransaction = new CustomerTransaction();
                bool result = customerTransaction.CreateCustomer(name, address, contactNumber, orders);

                if (result)
                {
                    
                    SendEmail(name, address, contactNumber, "Order Confirmation", $"Your order for {selectedJewelry.model} has been placed successfully.");
                    Console.WriteLine("Order placed and confirmation email sent.");
                }
                else
                {
                    Console.WriteLine("Order failed. Try again.");
                }
            }
            else
            {
                Console.WriteLine("Jewelry not found.");
            }
        }

        static void CancelOrder()
        {
            Console.WriteLine("\nEnter your full name to cancel the order:");
            string name = Console.ReadLine();

            CustomerTransaction customerTransaction = new CustomerTransaction();
            CustomerServices customerServices = new CustomerServices();

            Model foundCustomer = customerServices.GetUser(name);

            if (foundCustomer != null)
            {
                bool result = customerTransaction.DeleteUser(foundCustomer);

                if (result)
                {
                    SendEmail(foundCustomer.name, foundCustomer.address, foundCustomer.contactnumber, "Order Cancellation", "Your order has been canceled successfully.");
                    Console.WriteLine("Order canceled and cancellation email sent.");
                }
                else
                {
                    Console.WriteLine("Cancellation failed.");
                }
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        static void SendEmail(string name, string address, string contactNumber, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Maria's Jewelry Shop", "do-not-reply@mariasjewelry.com"));
            message.To.Add(new MailboxAddress(name, "user@example.com")); 
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = $"<h3>Hi, {name}!</h3><p>{messageBody}</p><p>Address: {address}</p><p>Contact: {contactNumber}</p> Thank you for purchasing at Maria's Jewelry! "
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("d9ed34b31df4d9", "8b7dcfdc441048");
                    client.Send(message);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}
