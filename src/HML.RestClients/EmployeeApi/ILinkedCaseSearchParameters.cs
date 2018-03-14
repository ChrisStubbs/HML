using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HML.RestClients.EmployeeApi
{
    public interface ILinkedCaseSearchParameters
    {
        Guid ClientId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime  Dob { get; set; }

    }
}
