using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using EmployeeManagement.API.Models;

namespace EmployeeManagement.api1.APIServices
{
    public class EmployeeService
    {
        public List<EmployeeModel> GetByDepartmentId(int departmentId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.GetAsync("http://localhost:81/api/Employee/GetByDepartmentId/" + departmentId).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var employeeModels = response.Content.ReadAsAsync<List<EmployeeModel>>().GetAwaiter().GetResult();

                    return employeeModels;
                }

                return null;
            }
        }

        public EmployeeModel GetByIdAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("http://localhost:81/api/Employee/GetById/" + id).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var employeeModel = response.Content.ReadAsAsync<EmployeeModel>().GetAwaiter().GetResult();

                    return employeeModel;
                }

                return null;
            }
        }
             
        public EmployeeModel Create(EmployeeModel employeeModel)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.PostAsJsonAsync("http://localhost:81/api/Employee/Create", employeeModel).GetAwaiter().GetResult();
            }

            return GetByIdAsync(employeeModel.Id);
        }

        public void Save(EmployeeModel employeeModel)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.PutAsJsonAsync("http://localhost:81/api/Employee/Update", employeeModel).GetAwaiter().GetResult();
            }
        }
    }
}
