using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;

        public int UserId { get; set; }

        public User User { get; set; } = null!;
    }
}