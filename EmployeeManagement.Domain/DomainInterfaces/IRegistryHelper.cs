namespace EmployeeManagement.Domain.DomainInterfaces
{
    public interface IRegistryHelper
    {
        void SetData(string name, string value);
        string GetData(string name);
        void RemoveData(string names);
    }
}
