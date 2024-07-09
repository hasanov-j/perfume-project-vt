using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GR_30321_Api.Data;
using GR_30321_Hasanov_Lb_3_Domain.Entities;
using GR_30321_Hasanov_Lb_3_Domain.Models;

namespace GR_30321_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfumesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PerfumesController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Perfumes
        [HttpGet]
        public async Task<ActionResult<ResponseData<ProductListModel<Perfume>>>> GetPerfumes(
            string? brand,
            int pageNumber = 1,
            int pageSize = 6
        ) {
            // Создать объект результата
            var result = new ResponseData<ProductListModel<Perfume>>();
            // Фильтрация по категории загрузка данных категории
            var data = _context.Perfumes
                .Include(p => p.Brand)
                .Where(p => String.IsNullOrEmpty(brand) || p.Brand.NormalizedName.Equals(brand));
            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);

            if (pageNumber > totalPages) pageNumber = totalPages;
            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new ProductListModel<Perfume>()
            {
                Items = await data
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),
                CurrentPage = pageNumber,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }

            return result;
        }

        // GET: api/Perfumes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfume>> GetPerfume(int id)
        {
            var perfume = await _context.Perfumes.FindAsync(id);

            if (perfume == null)
            {
                return NotFound();
            }

            return perfume;
        }

        // PUT: api/Perfumes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfume(int id, Perfume perfume)
        {
            if (id != perfume.Id)
            {
                return BadRequest();
            }

            _context.Entry(perfume).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfumeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Perfumes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Perfume>> PostPerfume(Perfume perfume)
        {
            _context.Perfumes.Add(perfume);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerfume", new { id = perfume.Id }, perfume);
        }

        // DELETE: api/Perfumes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfume(int id)
        {
            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume == null)
            {
                return NotFound();
            }

            _context.Perfumes.Remove(perfume);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfumeExists(int id)
        {
            return _context.Perfumes.Any(e => e.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(
            int id, 
            IFormFile image
        ) {
            // Найти объект по Id
            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume == null)
            {
                return NotFound();
            }
            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();
            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);
            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);
            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);
            // создать файл и открыть поток для записи
            using var stream = System.IO.File.OpenWrite(filePath);
            // скопировать файл в поток
            await image.CopyToAsync(stream);
            // получить Url хоста
            var host = "https://" + Request.Host;
            // Url файла изображения
            var url = $"{host}/Images/{fileName}";

            // Сохранить url файла в объекте
            perfume.Image = url;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
