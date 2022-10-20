using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyCRUD
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection; //readonly is so the only way we can give it value is through a constructor.

        public DapperDepartmentRepository(IDbConnection connection) //Constrouctor. //IdbConnection will help us connect to a database.
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            var depos = _connection.Query<Department>("SELECT * FROM departments;"); //.Query is selecting from the database.

            return depos;
        }

        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);", //the @ symbol is a variable.
            new { departmentName = newDepartmentName }); // An anon type. Kind of like using a lambda. But the program knows what it is. EX: x => x > 0;. 
        }
    }
}
