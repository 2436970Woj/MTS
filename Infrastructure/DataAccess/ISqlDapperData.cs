using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;
using System;

namespace Infrastructure.DataAccess;


/*  
    The ISqlDapperData interface defines two methods for working with an SQL database using Dapper. 
    The LoadSpData method is used to execute a stored procedure and retrieve a list of objects asynchronously, 
    while the SaveSpDate method is used to execute a stored procedure for saving data.
    The use of generics and async makes this interface flexible and suitable for various database
    operations while allowing for asynchronous execution.
*/

public interface ISqlDapperData
{
    string ConnectionStringName { get; set; }
    Task<List<T>> LoadSpData<T, U>(string storedProcedure, U parameters);
    Task SaveSpDate<T>(string storedProcedure, T parameters);

}


