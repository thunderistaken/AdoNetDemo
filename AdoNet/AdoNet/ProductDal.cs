using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ETrade; integrated security = true");
        public List<Product> GetAll()  //ADONET on DataTable
        {
            //step1: create sql connection object for adonet
            //step2:  string on this place ↑ note: '@' char for strings
            //connection string index : server name, witch db,  security (if using host uid name and pass)
            connectionControl();
            SqlCommand command = new SqlCommand("Select * from Products", _connection); //step4: we create another object for database query. we write the base query and the connection object
            SqlDataReader reader = command.ExecuteReader(); //commands are executing
            List<Product> products = new List<Product>(); //list objects for products
            while (reader.Read()) //read incoming records one by one
            {
                //we create a product object and receive the data from the database 
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),

                };
                products.Add(product); //and adding on list



            }


            reader.Close();            //close after reading
            _connection.Close();       // connection was closed
            return products;  // return the resul (list type)

            /*********************************************************************************/
            /*
       public DataTable GetAll2()  //ADONET on DataTable
       {
           //step1: create sql connection object for adonet
           SqlConnection connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ETrade; integrated security = true");
           //step2:  string on this place ↑ note: '@' char for strings
           //connection string index : server name, witch db,  security (if using host uid name and pass)
           if (connection.State == System.Data.ConnectionState.Closed)
           {
               connection.Open(); //step3:if connection was closed, open it.

           }
           SqlCommand command = new SqlCommand("Select * from Products", connection); //step4: we create another object for database query. we write the base query and the connection object
           SqlDataReader reader = command.ExecuteReader(); //commands are executing

           DataTable dataTable = new DataTable(); //data table object
           dataTable.Load(reader);        //data table is reading

           reader.Close();            //close after reading
           connection.Close();       // connection was closed
           return dataTable;    // return the resul (datatable type)



       }

       */

        }

        private void connectionControl()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open(); //step3:if connection was closed, open it.

            }
        }

        public void Add(Product product)
        {
            connectionControl();
            
            SqlCommand command = new SqlCommand("Insert into Products values(@name,@unitPrice,@stockAmount)",_connection);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery();
            _connection.Close();


        }

        public void Update(Product product)
        {
            connectionControl();

            SqlCommand command = new SqlCommand("Update Products set Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount where Id=@id", _connection);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();
            _connection.Close();


        }

        public void Delete(int id)
        {
            connectionControl();

            SqlCommand command = new SqlCommand("Delete from products where Id=@id", _connection);

           
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            _connection.Close();


        }

        public void DeleteAll()
        {

            connectionControl();

            SqlCommand command = new SqlCommand("Delete from products", _connection);


            
            command.ExecuteNonQuery();
            _connection.Close();
        }

    }
}
