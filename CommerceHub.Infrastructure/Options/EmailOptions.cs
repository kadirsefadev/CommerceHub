using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Options
{
    public class EmailOptions
    {
        public const string SectionName = "Email";
        public string FromAddress { get; set; }=string.Empty;
        public string FromName { get; set; } = string.Empty;
        public bool LogToDatabase { get; set; } = true;


    }
}
