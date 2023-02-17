using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem_ADO.Net
{
   public class AddressBookRespitory
    {
        //Give path for Database Connection
        public static string connection = @"Server=(localdb)\MSSQLLocalDB;Database=Address_Book;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection sqlConnection = new SqlConnection(connection);
        public int InsertIntoTable(ContactDataManager addressBook)
        {
            int result = 0;
            try
            {
                using (this.sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spInsertintoTable", this.sqlConnection);
                    //setting command type as stored procedure
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@FirstName", addressBook.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", addressBook.LastName);
                    sqlCommand.Parameters.AddWithValue("@Address", addressBook.Address);
                    sqlCommand.Parameters.AddWithValue("@City", addressBook.City);
                    sqlCommand.Parameters.AddWithValue("@State", addressBook.State);
                    sqlCommand.Parameters.AddWithValue("@zip", addressBook.zip);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", addressBook.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@Email", addressBook.Email);
                    sqlCommand.Parameters.AddWithValue("@Book_Name", addressBook.Book_Name);
                    sqlCommand.Parameters.AddWithValue("Contact_Type", addressBook.Contact_Type);
                    this.sqlConnection.Open();
                    //Return the number of rows updated
                    result = sqlCommand.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Updated");
                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return result;
        }

        //retrieve contact from database returns contact details of a person
        public bool GetContact(string name)
        {
            try
            {
                ContactDataManager addressBook = new ContactDataManager();
                using (this.sqlConnection)
                {
                    string query = @"Select * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);
                    this.sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        { 
                            addressBook.FirstName = Convert.ToString(dr["FirstName"]);
                            addressBook.FirstName = Convert.ToString(dr["FirstName"]);
                            addressBook.Address = Convert.ToString(dr["Address"] + " " + dr["City"] + " " + dr["State"] + " " + dr["zip"]);
                            addressBook.PhoneNumber = Convert.ToInt64(dr["PhoneNumber"]);
                            addressBook.Email = Convert.ToString(dr["Email"]);
                            addressBook.Book_Name = Convert.ToString(dr["Book_Name"]);
                            addressBook.Contact_Type = Convert.ToString(dr["ContactType"]);
                            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6}", addressBook.FirstName, addressBook.LastName, addressBook.Address, addressBook.PhoneNumber, addressBook.Email, addressBook.Book_Name, addressBook.Contact_Type);
                            System.Console.WriteLine("\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return false;
        }

        //UC03-edit contact of particular person
        public bool EditContact(string name, ContactDataManager contact)
        {
            SqlConnection sqlConnection = new SqlConnection(connection);
            try
            {
                int result;
                using (this.sqlConnection)
                {
                    string spName = "dbo.SpEditContact";
                    SqlCommand command = new SqlCommand(spName, sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    command.Parameters.AddWithValue("@LastName", contact.LastName);
                    command.Parameters.AddWithValue("@Address", contact.Address);
                    command.Parameters.AddWithValue("@City", contact.City);
                    command.Parameters.AddWithValue("@State", contact.State);
                    command.Parameters.AddWithValue("@Zip", contact.zip);
                    command.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", contact.Email);
                    command.Parameters.AddWithValue("@Book_Name", contact.Book_Name);
                    command.Parameters.AddWithValue("@Contact_Type", contact.Contact_Type);
                    sqlConnection.Open();
                    result = command.ExecuteNonQuery();
                }
                return result == 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return false;
        }
    }
}


