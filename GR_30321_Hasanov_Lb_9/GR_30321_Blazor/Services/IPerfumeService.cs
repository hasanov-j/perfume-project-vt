namespace GR_30321_Blazor.Services
{
    public interface IPerfumeService<T> where T : class
    {
        event Action ListChanged;

        // Список объектов
        IEnumerable<T> Perfumes { get; }
        // Номер текущей страницы
        int CurrentPage { get; }
        // Общее количество страниц
        int TotalPages { get; }
        // Получение списка объектов
        Task GetPerfumes(int pageNo = 1, int pageSize = 6);
    }
}
