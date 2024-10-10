using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomerLogBook;
using System.Data.SqlClient;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
namespace CustomerLogBookDL
    {//INTEG
        public class SqlDBData
    {
            string connectionString
            = "Data Source = LAPTOP-CQ7JC7SL\\SQLEXPRESS; Initial Catalog = CustomerLogBook; Integrated Security = True";
               

        SqlConnection sqlConnection;

        public SqlDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<Model> GetUsers()
        {
            string selectStatement = "SELECT name, address, contactnumber, orders FROM Model";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            List<Model> users = new List<Model>();

            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                string name = reader["name"].ToString();
                string address = reader["address"].ToString();
                string contactnumber = reader["contactnumber"].ToString();
                string orders = reader["orders"].ToString();

                Model readUser = new Model();
                readUser.name = name;
                readUser.address = address;
                readUser.contactnumber = contactnumber;
                readUser.orders = orders;
                users.Add(readUser);
            }

            sqlConnection.Close();

            return users;
        }

        public int AddCustomer(string name, string address, string contactnumber, string orders)
        {
            int success;

            string insertStatement = "INSERT INTO Model (name, address, contactnumber, orders) VALUES (@name, @address, @contactnumber, @orders)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@name", name);
            insertCommand.Parameters.AddWithValue("@address", address);
            insertCommand.Parameters.AddWithValue("@contactnumber", contactnumber);
            insertCommand.Parameters.AddWithValue("@orders", orders);

            sqlConnection.Open();
            success = insertCommand.ExecuteNonQuery();
            sqlConnection.Close();

            return success;
        }


        public int UpdateCustomer(string name, string address)
        {
            ;
            int success;
            string updateStatement = $"UPDATE Model SET address = @address WHERE name = @name";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            sqlConnection.Open();

            updateCommand.Parameters.AddWithValue("@Address", address);
            updateCommand.Parameters.AddWithValue("@name", name);
           

            success = updateCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }

        public int DeleteCustomer(string name)
        {
            int success;

            string deleteStatement = $"DELETE FROM Model WHERE name = @name";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            sqlConnection.Open();

            deleteCommand.Parameters.AddWithValue("@name", name);

            success = deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }
    }
        }