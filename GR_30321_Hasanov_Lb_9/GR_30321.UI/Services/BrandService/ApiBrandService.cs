using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace GR_30321.UI.Services.BrandService
{
    public class ApiBrandService(HttpClient httpClient) : IBrandService
    {
        public async Task<ResponseData<List<Brand>>> GetBrandListAsync()
        {
            var result = await httpClient.GetAsync(httpClient.BaseAddress);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<List<Brand>>>();
            };

            var response = new ResponseData<List<Brand>>
            { 
                Success = false, 
                ErrorMessage = "Ошибка чтения API" 
            };

            return response;
        }
    }
}
