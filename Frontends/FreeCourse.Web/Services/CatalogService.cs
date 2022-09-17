using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("Courses");

            if (response.IsSuccessStatusCode)
            {
                var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
                return responseSuccess.Data;
            }

            return null;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("Categories");

            if (response.IsSuccessStatusCode)
            {
                var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
                return responseSuccess.Data;
            }

            return null;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"Courses/GetAllByUserId/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
                return responseSuccess.Data;
            }

            return null;
        }

        public async Task<CourseViewModel> GetCourseByCourseIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"Courses/{courseId}");

            if (response.IsSuccessStatusCode)
            {
                var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
                return responseSuccess.Data;
            }

            return null;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("Courses", courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("Courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"Courses/{courseId}");

            return response.IsSuccessStatusCode;
        }
    }
}
