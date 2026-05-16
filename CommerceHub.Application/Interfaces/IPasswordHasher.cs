using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceHub.Infrastructure.Services
{
    public interface IPasswordHasher
    {
        string PasswordHash(string password);
        bool VerifyPassword(string password, string hash);
    }
}