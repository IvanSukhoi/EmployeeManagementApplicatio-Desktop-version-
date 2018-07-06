using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using EmployeeManagement.API.Models;

namespace EmployeeManagement.API.APIServices
{
    public class DepartmentService
    {
        public List<DepartmentModel> GetAll(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.GetAsync("http://localhost:81/api/Employee/GetByDepartmentId/" + id).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var departmentModels =
                        response.Content.ReadAsAsync<List<DepartmentModel>>().GetAwaiter().GetResult();

                    return departmentModels;
                }

                return null;
            }
        }
    }
}
