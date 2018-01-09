using LogService.Configuration;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Log.Repository
{

    public class DataBaseRepository : IDisposable
    {
        string _connectionString;

        SqlConnection _connection;

        SqlCommand _command;

        public DataBaseRepository()
        {
            var keystringConexao = LogServiceConfiguration.Instance.Conexao;

            _connectionString = ConfigurationManager.ConnectionStrings[keystringConexao].ConnectionString;
        }

        public IDataReader Query(string query, SqlParameter[] parametros,
            CommandType tipoDeComando)
        {
            _connection = new SqlConnection(_connectionString);
            _command = new SqlCommand(query, _connection);
            _command.CommandType = tipoDeComando;
            _command.Parameters.AddRange(parametros);
            try
            {
                _connection.Open();
                return _command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void NonQuery(string query, SqlParameter[] parameters,
            CommandType typeCommand)
        {

            _connection = new SqlConnection(_connectionString);
            _command = new SqlCommand(query, _connection);

            _command.CommandType = typeCommand;
            _command.Parameters.AddRange(parameters);

            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        private void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }

        public void Dispose()
        {
            CloseConnection();
            _command.Dispose();
        }
    }

}
