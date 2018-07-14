using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IRegistryHelper
    {
        void SetData(string name, string value);
        string GetData(string name);
        void RemoveData(string name, string value);
    }
}
