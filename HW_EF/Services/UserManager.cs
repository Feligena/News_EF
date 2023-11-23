using HW_EF.Encryptors;
using HW_EF.Middlewares.Services;
using HW_EF.Models;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HW_EF.Services
{
    public class UserManager : IUserManager
    {
        private readonly BlogDbContext userDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserManager(BlogDbContext userDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.userDbContext = userDbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public PersonsCredentials GetCredentials()
        {
            if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
            {
                var hash = httpContextAccessor.HttpContext.Request.Cookies["auth"];
                var json = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
                return JsonSerializer.Deserialize<PersonsCredentials>(json);
            }
            else
            {
                return null;
            }
        }

        public bool Login(string username, string password)
        {
            //CHECK
            var passwordHash = Sha256Encryptor.Encrypt(password);
            var user = userDbContext.Persons.FirstOrDefault(x => x.Email == username && x.PasswordHash == passwordHash);
            if (user != null)
            {
                PersonsCredentials usersCredentials = new PersonsCredentials()
                {
                    IsAdmin = user.IsAdmin,
                    Email = user.Email
                };
                var json = JsonSerializer.Serialize(usersCredentials);
                var hash = AesEncryptor.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", json);

                httpContextAccessor.HttpContext.Response.Cookies.Append("auth", hash);
                return true;
            }
            return false;
        }
    }
}
