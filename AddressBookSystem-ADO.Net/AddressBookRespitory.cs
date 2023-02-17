using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem_ADO.Net
{
    class AddressBookRespitory
    {
        //Give path for Database Connection
        public static string connection = @"Server=(localdb)\MSSQLLocalDB;Database=Address_Book;Trusted_Connection=True;";
        //Represents a connection to Sql Server Database
        SqlConnection sqlConnection = new SqlConnection(connection);
    }
}
