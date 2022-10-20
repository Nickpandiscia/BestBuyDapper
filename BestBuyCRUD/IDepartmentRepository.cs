using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyCRUD
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments(); //Stubbed out.
        //Method GetAllDepartments(); that conforms to an IEnumerable<Departments>.

        void InsertDepartment(string newDepartmentName);


    }
}
