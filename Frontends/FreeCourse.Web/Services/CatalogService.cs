﻿using FreeCourse.Shared.Dtos;
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
        private readonly IPhotoStockService _photoStockService;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("Courses/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
                return responseSuccess.Data;
            }

            return null;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("Categories/GetAll");

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
            var response = await _httpClient.GetAsync($"Courses/GetById/{courseId}");

            if (response.IsSuccessStatusCode)
            {
                var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
                return responseSuccess.Data;
            }

            return null;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                courseCreateInput.Picture = resultPhotoService.Url;
            }

            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("Courses/Create", courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("Courses/Update", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"Courses/Delete/{courseId}");

            return response.IsSuccessStatusCode;
        }
    }
}
