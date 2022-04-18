using System.Data;
using System.Data.Common;


namespace ZToolbox
{
    public class Connection
    {
        private readonly string _connectionString;
        private readonly DbProviderFactory _factory;

        public Connection(string connectionString, DbProviderFactory factory)
        {
            _connectionString = connectionString;
            _factory = factory;

            using (IDbConnection dbConnection = CreateConnection(_factory, _connectionString))
            {
                dbConnection.Open();
            }
        }

        private static IDbConnection CreateConnection(DbProviderFactory factory, string connectionString)
        {
            IDbConnection? connection = factory.CreateConnection();
            if (connection is null)
                throw new InvalidOperationException();

            connection.ConnectionString = connectionString;
            return connection;
        }

        private static IDbCommand CreateCommand(IDbConnection dbConnection, Command command)
        {
            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = command.Query;
            dbCommand.CommandType = command.IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

            foreach (KeyValuePair<string, object> kvp in command.Parameters)
            {
                IDbDataParameter dbDataParameter = dbCommand.CreateParameter();
                dbDataParameter.ParameterName = kvp.Key;
                dbDataParameter.Value = kvp.Value;
                dbCommand.Parameters.Add(dbDataParameter);
            }
            return dbCommand;
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataReader, TResult> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            using (IDbConnection dbConnection = CreateConnection(_factory, _connectionString))
            {
                using (IDbCommand dbCommand = CreateCommand(dbConnection, command))
                {
                    dbConnection.Open();
                    using (IDataReader reader = dbCommand.ExecuteReader())
                    {
                        while (reader.Read())
                            yield return selector(reader);
                    }
                }
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (IDbConnection dbConnection = CreateConnection(_factory, _connectionString))
            {
                using (IDbCommand dbCommand = CreateCommand(dbConnection, command))
                {
                    dbConnection.Open();
                    return dbCommand.ExecuteNonQuery();
                }
            }
        }

        public object? ExecuteScalar(Command command)
        {
            using IDbConnection dbConnection = CreateConnection(_factory, _connectionString);
            using IDbCommand dbCommand = CreateCommand(dbConnection, command);
            dbConnection.Open();
            object? o = dbCommand.ExecuteScalar();
            return o is DBNull ? null : o;
        }
    }
}
