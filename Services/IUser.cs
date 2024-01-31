using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUser
    {
        public string AddUserRole(string password);
        public Task<string> UserRegisterToDB(User user);
        public Task<string> UpdateUserInDB(int id,UserUpdateData user);
        public Task<string> DeleteUserInDB(int id);
        public Task<User> UserValidation(LoginData loginData);
    }
}
