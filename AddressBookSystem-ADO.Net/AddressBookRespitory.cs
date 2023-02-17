﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    // //UseCase 4-Delete Contact using their name
    public int DeletePersonBasedonName()
    {
        SqlConnection sqlConnection = new SqlConnection(connection);
        try
        {
            using (this.sqlConnection)
            {
               
                
                string query = "delete from AddressBookDetails where FirstName = 'Rinku' and LastName = 'Berde'";
                //Pass query to TSql
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                string spName = "dbo.SpDeleteContact";
                SqlCommand command = new SqlCommand(spName, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Updated!");
                }
                else
                {
                    Console.WriteLine("Not Updated!");
                }
                return result;
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
    //UseCase 5:ability to Retrieve Person belonging to a City or State from the Address Book
    public string PrintDataBasedOnCity(string city, string State)
    {
        string nameList = "";
        //query to be executed
        string query = @"select * from AddressBookDetails where City =" + "'" + city + "' or State=" + "'" + State + "'";
        SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
        sqlConnection.Open();
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                DisplayEmployeeDetails(sqlDataReader);
                nameList += sqlDataReader["FirstName"].ToString() + " ";
            }
        }
        return nameList;
    }

    //  UC-08-ability to get size of contact using city
    public bool GetSize()
    {
        SqlConnection connection = new SqlConnection(sqlConnection);
        try
        {
            using (sqlConnection)
            {
                string spName = "dbo.SpGetSize";
                SqlCommand command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"Number of cities: {dr.GetInt32(0)} \n Number of states: {dr.GetInt32(1)}");
                }
                return dr != null ? true : false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return false;
    }

   // UC-09-sorted alphabetically by Person’s name for a given city
    public bool SortTable(string city)
    {
        SqlConnection connection = new SqlConnection(sqlConnection);
        try
        {
            int flag = 0;
            using (sqlConnection)
            {
                string spName = "dbo.SpSortTable";
                SqlCommand command = new SqlCommand(spName, connection);
                command.CommandType = CommandType.StoredProcedure;
                connection.Open();
                command.Parameters.AddWithValue("@City", city);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    flag++;
                    FirstName = dr.GetString(0);
                    LastName = dr.GetString(1);
                    Address = dr.GetString(2);
                    City = dr.GetString(3);
                    State = dr.GetString(4);
                    zip = dr.GetInt32(5);
                    PhoneNumber = dr.GetInt64(6);
                    Email = dr.GetString(7);
                    Book_Name = dr.GetString(2);
                    Contact_Type = dr.GetString(2);
                    Console.WriteLine($"{FirstName} {LastName} {Address} {City} {State} {zip} {PhoneNumber} {Email} {Book_Name} {Contact_Type}" );
                }
                outputMessage = flag >= 1 ? $"{flag} Contact(s) found" : "Contact not found";
                Console.WriteLine(outputMessage);
                return flag >= 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return false;
    }

    //UC 10: Ability to get number of contact persons by Type
    public string ContactDataBasedOnType()
    {
        string nameList = "";
        //query to be executed
        string query = @"select Count(*) as NumberOfContacts,Type from AddressBookDetails Group by Type";
        SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
        sqlConnection.Open();
        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                Console.WriteLine("{0} \t {1}", sqlDataReader[0], sqlDataReader[1]);
                nameList += sqlDataReader[0].ToString() + " ";
            }
        }
        return nameList;
    }
}






