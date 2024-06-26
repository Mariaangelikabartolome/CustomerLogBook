using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomerLogBook;
using System.Data.SqlClient;
namespace CustomerLogBookDL
    {
        public class SqlDBData
        {
            string connectionString
               = "Data Source =LAPTOP-VESUE4DG\\SQLEXPRESS01; Initial Catalog = CustomerLogBook; Integrated Security = True;";

        SqlConnection sqlConnection;

        public SqlDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public List<Model> GetUsers()
        {
            string selectStatement = "SELECT name, address, contactnumber FROM Model";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            List<Model> users = new List<Model>();

            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                string name = reader["name"].ToString();
                string address = reader["address"].ToString();
                string contactnumber = reader["contactnumber"].ToString();

                Model readUser = new Model();
                readUser.name = name;
                readUser.address = address;
                readUser.contactnumber = contactnumber;
                users.Add(readUser);
            }

            sqlConnection.Close();

            return users;
        }

        public int AddCustomer(string name, string address, string contactnumber )
        {
            int success;

            string insertStatement = "INSERT INTO users VALUES (@name, @address, @contactnumber)";

            SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@name", name);
            insertCommand.Parameters.AddWithValue("@address", address);
            insertCommand.Parameters.AddWithValue("@contactnumber", contactnumber);
            sqlConnection.Open();

            success = insertCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }

        public int UpdateCustomer(string name, string address, string contactnumber)
        {
            int success;

            string updateStatement = $"UPDATE users SET address = @address WHERE name = @name";
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

            string deleteStatement = $"DELETE FROM users WHERE name = @name";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            sqlConnection.Open();

            deleteCommand.Parameters.AddWithValue("@name", name);

            success = deleteCommand.ExecuteNonQuery();

            sqlConnection.Close();

            return success;
        }
    }
        }