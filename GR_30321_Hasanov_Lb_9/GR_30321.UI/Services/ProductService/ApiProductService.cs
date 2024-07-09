using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;
using System.Net.Http;
using System.Text.Json;

namespace GR_30321.UI.Services.ProductService
{
    public class ApiProductService(HttpClient httpClient) : IProductService
    {
        public async Task<ResponseData<Perfume>> CreateProductAsync(
            
            Perfume product, 
            IFormFile? formFile
        ) {

            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            // Подготовить объект, возвращаемый методом
            var responseData = new ResponseData<Perfume>();
            // Послать запрос к API для сохранения объекта
            var response = await httpClient.PostAsJsonAsync(httpClient.BaseAddress, product);
            if (!response.IsSuccessStatusCode)
            {
                responseData.Success = false;
                responseData.ErrorMessage = $"Не удалось создать объект:{response.StatusCode}";

                return responseData;
            }

            // Если файл изображения передан клиентом
            if (formFile != null)
            {
                // получить созданный объект из ответа Api-сервиса
                var perfume = await response.Content.ReadFromJsonAsync<Perfume>();
                // создать объект запроса
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}{perfume.Id}")
                };
                // Создать контент типа multipart form-data
                var content = new MultipartFormDataContent();
                // создать потоковый контент из переданного файла
                var streamContent = new StreamContent(formFile.OpenReadStream());
                // добавить потоковый контент в общий контент по именем "image"
                content.Add(streamContent, "image", formFile.FileName);
                // поместить контент в запрос
                request.Content = content;
                // послать запрос к Api-сервису
                response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    responseData.Success = false;
                    responseData.ErrorMessage = $"Не удалось сохранить изображение:{response.StatusCode}";
                }
            }

            return responseData;
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Perfume>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ProductListModel<Perfume>>> GetProductListAsync(
            string? brandNormalizedName, 
            int pageNumber = 1
        ) {

            var uri = httpClient.BaseAddress;
            var queryData = new Dictionary<string, string>();
            queryData.Add("pageNumber", pageNumber.ToString());
            if (!String.IsNullOrEmpty(brandNormalizedName))
            {
                queryData.Add("brand", brandNormalizedName);
            }
            var query = QueryString.Create(queryData);

            var result = await httpClient.GetAsync(uri + query.Value);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Perfume>>>();
            };
            var response = new ResponseData<ProductListModel<Perfume>>
            { 
                Success = false, 
                ErrorMessage = "Ошибка чтения API" 
            };

            return response;
        }

        public Task UpdateProductAsync(int id, Perfume product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
