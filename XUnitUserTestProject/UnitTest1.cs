using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace XUnitUserTestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetUsersTest()
        {
            var apiClient = new HttpClient();
            var apiResponse = await apiClient.GetAsync("https://reqres.in/api/users");
            var json = JsonDocument.Parse(await apiResponse.Content.ReadAsStringAsync());
            IEnumerable<dynamic> userList = null;
           if(json.RootElement.TryGetProperty("data", out var data))
            {
                 userList = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(data.ToString());
            }

            Assert.True(apiResponse.IsSuccessStatusCode);
            Assert.True(userList.Count() == 6);

        }
        [Fact]
        public async Task GetExistingUserTest()
        {
            var apiClient = new HttpClient();
            var apiResponse = await apiClient.GetAsync("https://reqres.in/api/users/2");
            var test = JsonConvert.SerializeObject(apiResponse);
            Assert.True(apiResponse.IsSuccessStatusCode);

        }
        [Fact]
        public async Task GetNonExistingUserTest()
        {
            var apiClient = new HttpClient();
            var apiResponse = await apiClient.GetAsync("https://reqres.in/api/users/23");


            Assert.True(apiResponse.StatusCode == HttpStatusCode.NotFound);

        }
        [Fact]
        public async Task PostNewUserTest()
        {
            var apiClient = new HttpClient();
            var _email = "test@yahoo.com";
            var _password = "password123";
            var content = new StringContent($"email={_email}&password={_password}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var apiResponse = await apiClient.PostAsync("https://reqres.in/api/registeruser", content);
            Assert.True(apiResponse.StatusCode == HttpStatusCode.Created);

        }
    }
}
