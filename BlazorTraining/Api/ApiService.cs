using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorTraining.Api
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration.GetSection("ApiUrl").Value ?? throw new Exception("Api Url is null in app setting."));
        }

        public async Task<T> Execute<T>(string endpoint, object requestModel)
        {
            T model = default;
            string requestJsonStr = JsonConvert.SerializeObject(requestModel);
            HttpContent content = new StringContent(requestJsonStr, Encoding.UTF8, Application.Json);
            var response = await _httpClient.PostAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            else
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                throw new Exception($"Api Error {response.StatusCode} | {jsonStr}");
            }
            return model;
        }

        public async Task<BlogListResponseModel> GetBlogs(BlogRequestModel requestModel)
        {
            return await Execute<BlogListResponseModel>(ApiEndpoints.GetBlogs, requestModel);
            //BlogListResponseModel model = new BlogListResponseModel();
            //string requestJsonStr = JsonConvert.SerializeObject(requestModel);
            //HttpContent content = new StringContent(requestJsonStr, Encoding.UTF8, Application.Json);
            //_httpClient.BaseAddress = new Uri(_configuration.GetSection("ApiUrl").Value ?? throw new Exception("Api Url is null in app setting."));
            //var response = await _httpClient.PostAsync(ApiEndpoints.GetBlogs, content);
            //if (response.IsSuccessStatusCode)
            //{
            //    var jsonStr = await response.Content.ReadAsStringAsync();
            //    model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);
            //}
            //else
            //{
            //    var jsonStr = await response.Content.ReadAsStringAsync();
            //    throw new Exception($"Api Error {response.StatusCode} | {jsonStr}");
            //}
            //return model;
        }

        public async Task<BlogResponseModel> CreateBlog(BlogRequestModel requestModel)
        {
            return await Execute<BlogResponseModel>(ApiEndpoints.CreateBlog, requestModel);
        }
    }
}
