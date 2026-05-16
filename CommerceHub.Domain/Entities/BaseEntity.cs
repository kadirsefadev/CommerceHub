using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Domain.Entities
{
    //Tüm entity'lerin ortak alanlar burada biz burada Dry prensibine uygun olarak ortak alanları tanımlayarak kod tekrarını önlüyoruz.
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
