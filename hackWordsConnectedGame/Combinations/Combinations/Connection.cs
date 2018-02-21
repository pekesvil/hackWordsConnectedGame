using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Combinations
{
    public class Connection
    {

        public readonly string _conexionString;
        //variable que servirá para determinar si estamos conectados a
        //la base de datos.
        private bool _connection;
        private SqlDataAdapter _dbAdapter;
        //Objeto de  comando
        private SqlCommand _dbCommand;
        //Objeto de conexion
        private SqlConnection _dbConnection;
        private SqlDataReader _dReader;
        private DataSet _ds;
        //public  string _nombreConexion;
        /*
		 * public BaseDatos()
		 * Costructor que inicializa la variable de instancia Connection a
		 * false.
		 */

        public string ConnectionString(string nombreConeccion)
        {
            return ConfigurationManager.ConnectionStrings[nombreConeccion].ConnectionString;
        }

        public Connection()
        {
            _connection = false;
        }

        public Connection(string nombreConexion)
        {
            _connection = false;
            string conexionString = ConnectionString(nombreConexion);//System.Configuration.ConfigurationManager.ConnectionStrings[nombreConexion].ConnectionString;
            _conexionString = conexionString;
            //ConfigurationManager.ConnectionStrings[nombreConexion].ConnectionString;
            /*
            switch (NombreConexion)
            {

                case "SEPTIMUS2":
                    conexionString = "Data Source=ECTEST012Q43;Initial Catalog=Septimus2; Trusted_Connection=TRUE";
                    break;
                case "ConneccionNewODT":
                    conexionString =
                        "Data Source=ecbpprq54,1454;Initial Catalog=MDB;Max Pool Size=25;Min Pool Size=3;Pooling=true;connection reset=true;connection timeout=3600;User ID=USRPRMDB;Password=SD*2012MDBchvc";
                    break;
                case "ConneccionLibr":
                    conexionString =
                        "Data Source=ECUIO012Q03;Initial Catalog=BSDT;Max Pool Size=25;Min Pool Size=3;Pooling=true;connection reset=true;connection timeout=3600;User ID=usubbsdt;Password=Pichincha1";
                    break;
                case "ConneccionLibrBGR":
                    conexionString =
                        "Data Source=ecuio012c22;Initial Catalog=BSDTBGR;Max Pool Size=25;Min Pool Size=3;Pooling=true;connection reset=true;connection timeout=3600;User ID=usubbsdt;Password=Bsdt5.bGr";
                    break;
                case "PPONCEDB":
                    conexionString = "Data Source=BT023289;Initial Catalog=PPONCEDB; Trusted_Connection=TRUE";
                    break;
            }
            */
        }

        private DataSet GetData(string sql)
        {
            GetConnection();
            _dbAdapter = new SqlDataAdapter(sql, _dbConnection);
            _ds = new DataSet();
            _dbAdapter.Fill(_ds); //, "Agenda");
            CloseConnection();
            return (_ds);
        }

        /*public void GetConnection()
		 *Método que nos permite conectarnos a una base de datos
		 *Parametros de entrada:
		 *Parametros de salida: */

        private void GetConnection()
        {
            //Cadena de conexión a la base de datos
            //string str_connection = _conexionString;

            if (_connection)
                return;
            //if (Connection == false)
            //{
            //Crea un objeto de conexión.
            _dbConnection = new SqlConnection(_conexionString); //new SqlConnection(str_connection);
            //Se abre la conexión a la Base de Datos
            _dbConnection.Open();
            _connection = true;
            //}
        }

        /*public void CloseConnection()
		 *Método que nos permite desconectarnos de la base de datos
		 *Parametros de entrada:
		 *Parametros de salida: */

        private void CloseConnection()
        {
            //Cerramos la conexión a la base de datos
            _dbConnection.Close();
            _connection = false;
        }

        /*public bool EjecutarSql(string sql)
		 *
		 *Método que nos permite ejecutar un comando sql
		 *Parametros de entrada:
		 * sql tipo string que guarda un comando a ejecutarse en la BD
		 *Parametros de salida:
		 * true si la sentensia sql fue ejecutada satisfactoriamente
		 * false caso contrario y lanzamos una excepción.
		 */

        private bool EjecutarSql(string sql)
        {
            try
            {
                //Nos conectamos a la Base de datos
                GetConnection();
                //creamos un objeto comando
                _dbCommand = new SqlCommand(sql, _dbConnection);
                //ejecutamos la sentencia sql
                _dbCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error to executed the transaction! " + e.Message);
                return false;
            }
            finally
            {
                //Nos desconectamos de la base de datos
                CloseConnection();
            }
        }

        /*public string connString()
		 *
		 *Método que retorna la cadena de conexion a una base de datos.
		 *Parametros de entrada:
		 *Parametros de salida:
		 * conexionString tipo string que contiene la cadena de conexión
		 */

        private string ConnString()
        {
            return _conexionString;
        }

        /*public SqlDataReader returnReader(string sql)
		 *
		 *Método que nos permite leer la base de datos.
		 *Parametros de entrada:
		 * sql tipo string que guarda un comando a ejecutarse en la BD
		 *Parametros de salida:
		 * dReader tipo SqlDataAdapter
		 */

        private SqlDataReader ReturnReader(string sql)
        {
            try
            {
                //Nos conectamos a la Base de datos
                GetConnection();
                //creamos un objeto comando
                _dbCommand = new SqlCommand(sql, _dbConnection);
                //ejecutamos la sentencia sql
                _dReader = _dbCommand.ExecuteReader();
                _dReader.Read();
                return _dReader;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error to executed the transaction! " + e.Message);
                return _dReader;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet ExecuteStoredProcedure(string storedProcedureName, params object[] parameterValues)
        {
            var consulta = new DataSet();
            try
            {
                // Open Connection
                GetConnection();
                var command = _dbConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;

                // Discover Parameters for Stored Procedure
                // Populate command.Parameters Collection.
                // Causes Rountrip to Database.
                SqlCommandBuilder.DeriveParameters(command);

                // Initialize Index of parameterValues Array
                var index = 0;

                // Populate the Input Parameters With Values Provided        
                foreach (
                    var parameter in
                        command.Parameters.Cast<SqlParameter>()
                            .Where(parameter => parameter.Direction == ParameterDirection.Input ||
                                                parameter.Direction == ParameterDirection.InputOutput ||
                                                parameter.Direction == ParameterDirection.Output
                                                ))
                {
                    parameter.Value = parameterValues[index];
                    index++;
                }

                var da = new SqlDataAdapter { SelectCommand = command };
                da.Fill(consulta);
            }
            catch (Exception ex)
            {
                var msj = ex.Message;
                throw ex;
            }
            finally
            {
                //Nos desconectamos de la base de datos
                CloseConnection();
            }
            return consulta;
        }

        public DataSet ExecuteStoredProcedure(string storedProcedureName)
        {
            var consulta = new DataSet();
            try
            {
                // Open Connection
                GetConnection();
                var command = _dbConnection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storedProcedureName;

                var da = new SqlDataAdapter { SelectCommand = command };
                da.Fill(consulta);
            }
            catch (Exception ex)
            {
                var msj = ex.Message;
            }
            finally
            {
                //Nos desconectamos de la base de datos
                CloseConnection();
            }
            return consulta;
        }

        public bool ExecuteScalar(string StoreProcedure, string[] parameterNames, object[] parameterValues)
        {
            //using (SqlConnection conn = new SqlConnection(_conexionString))
            GetConnection();
            try
            {
                using (SqlCommand cmd = new SqlCommand(StoreProcedure, _dbConnection))
                {
                    // define command to be stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    int positionpValues = 0;
                    // add parameter
                    foreach (var obj in parameterNames)
                    {
                        cmd.Parameters.Add(obj, SqlDbType.NVarChar).Value = parameterValues[positionpValues++];
                    }
                    // open connection, execute command, close connection

                    int result = (int)cmd.ExecuteScalar();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool ExecuteScalar_OutputParameter(string StoreProcedure, string[] parameterNames, object[] parameterValues)
        {

            try
            {
                GetConnection();
                using (SqlCommand cmd = new SqlCommand(StoreProcedure, _dbConnection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(parameterNames[0], parameterValues[0]);
                    SqlParameter outputParamenter = new SqlParameter("@RESULT", SqlDbType.Bit);
                    outputParamenter.Direction = ParameterDirection.Output;
                    var returnP = cmd.Parameters.Add(outputParamenter);
                    cmd.ExecuteNonQuery();
                    var result = returnP.Value;
                    return Convert.ToBoolean(result);
                }
            }
            catch (Exception ex)
            {
                string msj = ex.Message;
                return false;
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}