using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.DataAccess;

//This C# class demonstrates the setup of a class for working with a SQL database using Dapper
public class SqlDapperData : ISqlDapperData
{
    /*
        This line declares a private, read-only field _config of type IConfiguration. _config is used to store configuration settings,
        which are typically used to connect to a database or configure other aspects of the application.
    */

    private readonly IConfiguration _config;
    private string _connectionStringName = "DefaultConnection";
    /*
        This line defines a public property named ConnectionStringName.
        This property has a getter and a setter.The default value for this property is set to "DefaultConnection."
    */
    /*
        This is the constructor of the class, which takes an IConfiguration object config as a parameter.
        The IConfiguration interface is typically used in .NET applications to read configuration settings,
        such as connection strings, from configuration files(e.g., appsettings.json).
    */
    public string ConnectionStringName
    {
        get { return _connectionStringName; }
        set { _connectionStringName = value; }
    }

    public SqlDapperData(IConfiguration config)
    {
        _config = config;
    }

    /*
        two methods load and save will cover all Sql database CRUD with Dapper. I spli these up into 4 for demo purposes
        this is a generic repository pattern as the same two methods are used for all queries and commands
        parameters: string storedProcedure, U parameters (Can pass in pretty much any type)
    */
    public async Task<List<T>> LoadSpData<T, U>(string storedProcedure, U parameters)
    {
        //method signature 
        // 1. Asyncronist
        // 2. Returns Task which all async methods should do
        // 3. the Task is type IEnumerable of T, with T being the first generic in the method
        // it is whatever type we want to return for example the Employee entity/model will be passed in as T
        // saying we want an IEnumerable (which is a set) of this model returned from a db call to SQL
        // pass Employee Model (T) and the stored procedure is: sp name - this is for Sql based queries not commands
        // 4. The are parameters (U) if you did a get by Id the Id would be the parameter passed
        // 5. connectionId is the name of the Db connection string in Api appsettings.json

        string? connectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        var data = await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        return data.ToList();

    }


    public async Task SaveSpDate<T>(string storedProcedure, T parameters)
    {
        string? connectionString = _config.GetConnectionString(ConnectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);

        //This is ExecuteAsync because no data is being returned 
        // pass in stored procedure, parameters and command type stored procedure

        await connection.ExecuteAsync(
            storedProcedure,
            parameters,
            commandType: CommandType.StoredProcedure);
    }

}
