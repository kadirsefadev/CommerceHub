using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Options
{
    public class JwtOptions
    {
        public const string SectionName ="Jwt";
        public string Secret {  get; set; }= string.Empty;
        public string Issuer {  get; set; }= string.Empty;
        public string Audience {  get; set; }= string.Empty;
        public int ExpiresInMinutes {  get; set; }= 60;

    }
}
