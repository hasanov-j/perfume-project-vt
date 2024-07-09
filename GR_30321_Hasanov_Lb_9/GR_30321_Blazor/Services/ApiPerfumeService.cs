using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;
using System.Collections.ObjectModel;
using static System.Net.WebRequestMethods;

namespace GR_30321_Blazor.Services
{
    public class ApiPerfumeService(HttpClient httpClient) : IPerfumeService<Perfume>
    {
        public event Action ListChanged;
        private List<Perfume> _perfumes;
        int _currentPage = 1;
        int _totalPages = 1;
        public IEnumerable<Perfume> Perfumes => _perfumes;

        public int CurrentPage => _currentPage;

        public int TotalPages => _totalPages;

        public async Task GetPerfumes(int pageNo = 1, int pageSize = 6)
        {
            // Url сервиса API
            var uri = httpClient.BaseAddress.AbsoluteUri;
            // данные для Query запроса
            var queryData = new Dictionary<string, string>
            {
                { "pageNumber", pageNo.ToString() },
                {"pageSize", pageSize.ToString() }
            };

            var query = QueryString.Create(queryData);
            // Отправить запрос http
            var result = await httpClient.GetAsync(uri + query.Value);
            // В случае успешного ответа
            if (result.IsSuccessStatusCode)
            {
                // получить данные из ответа
                var responseData = await result.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Perfume>>>();

                // обновить параметры
                _currentPage = responseData.Data.CurrentPage;
                _totalPages = responseData.Data.TotalPages;
                _perfumes = responseData.Data.Items;
                ListChanged?.Invoke();
            }
            else // В случае ошибки
            {
                _perfumes = null;
                _currentPage = 1;
                _totalPages = 1;
            }
        }
    }
}
