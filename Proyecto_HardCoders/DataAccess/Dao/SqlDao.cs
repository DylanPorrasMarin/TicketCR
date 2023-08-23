using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class SqlDao
    {
        private static SqlDao instance = new SqlDao();

       // private string _connString = "Server=tcp:bdticketcr.database.windows.net,1433;Initial Catalog=bdticketcroficial;Persist Security Info=False;User ID=admin_ticket;Password=Djpm1203;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private string _connString = "Server=DYLAN-LAP;Database=TicketCR;Trusted_Connection=True";
        //private string _connString = "Server=LAPTOP-FN5GAS7U\\SQLEXPRESS;Database=TicketCR;Trusted_Connection=True";
        //private string _connString = "Server=DYLAN-LAP;Database=TicketCR;Trusted_Connection=True";
      // private string _connString = "Server=LAPTOP-FN5GAS7U\\SQLEXPRESS;Database=TicketCR;Trusted_Connection=True";
        //private string _connString = "Server=LAPTOP-0BTFFL56;Database=TicketCR;Trusted_Connection=True";
        //private string _connString = "Server=PAME;Database=TicketCR;Trusted_Connection=True";
       //private string _connString = "Server=LAPTOP-6FOOJFRF;Database=TicketCR;Trusted_Connection=True";



        public static SqlDao GetInstance()
        {
            if (instance == null)
                instance = new SqlDao();
            return instance;
        }

        //Metodo que conecta a la Base de Datos, ejecuta un stored procedure
        //que no devuelve ningun dato a la applicacion
        public void ExecuteStoredProcedure(SqlOperation operation)
        {
            string connectionString = _connString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.CommandText = operation.ProcedureName;
                command.CommandType = CommandType.StoredProcedure;

                foreach (var p in operation.parameters)
                {
                    command.Parameters.Add(p);
                }

                conn.Open();
                command.ExecuteNonQuery();
            } // La conexión se cerrará automáticamente al salir del bloque 'using'
        }


        public List<Dictionary<string, object>> ExecuteStoredProcedureWithQuery(SqlOperation operation)
        {
            var conn = _connString;
            List<Dictionary<string, object>> lstResults = new List<Dictionary<string, object>>();

            var connection = new SqlConnection(conn);
            var command = new SqlCommand();

            //preparar el comando a ejecutar
            command.Connection = connection;
            command.CommandText = operation.ProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            //Agregar los parametros
            foreach (var p in operation.parameters)
            {
                command.Parameters.Add(p);
            }

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            //Recorrer el resultado para poder armar la Lista de diccionarios
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Dictionary<string, object> diccObj = new Dictionary<string, object>();

                    for (var fieldCount = 0; fieldCount < reader.FieldCount; fieldCount++)
                    {
                        diccObj.Add(reader.GetName(fieldCount), reader.GetValue(fieldCount));
                    }

                    lstResults.Add(diccObj);
                }
            }
            reader.Close();

            return lstResults;

        }

		internal object ExecuteStoredProcedureWithQuery(object operation)
		{
			throw new NotImplementedException();
		}
	}
}
