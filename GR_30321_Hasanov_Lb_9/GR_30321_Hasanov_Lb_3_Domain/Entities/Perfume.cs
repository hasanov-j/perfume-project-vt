using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_30321_Hasanov_Lb_3_Domain.Entities
{
    public class Perfume
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; }
        public int BrandId { get; set; } // Внешний ключ для связи с брендом
        public Brand? Brand { get; set; } // Навигационное свойство к бренду
    }
}
