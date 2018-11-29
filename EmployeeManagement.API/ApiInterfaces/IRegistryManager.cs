namespace EmployeeManagement.API.ApiInterfaces
{
    public interface IRegistryManager
    {
        void SetData(string name, string value);
        string GetData(string name);
        void RemoveData(string names);
    }
}
