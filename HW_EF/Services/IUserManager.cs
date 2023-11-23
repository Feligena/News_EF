using HW_EF.Models;

namespace HW_EF.Middlewares
.Services
{
    public interface IUserManager
    {
        bool Login(string username, string password);
        PersonsCredentials GetCredentials();
    }
}
