using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUser
    {
        private readonly MyData _myData;
        public UserService(MyData myData)
        {
            _myData= myData;
        }
        public string AddUserRole(string password)
        {
            if (password.Substring(0, 2) == "ad")
                return "admin";
            else
                return "user";
            
        }

        
        public async Task<string> UserRegisterToDB(User user)
        {
            await _myData.users.AddAsync(user);
            await _myData.SaveChangesAsync();
            return "User is Registered!";
        }
        public async Task<string> UpdateUserInDB(int id, UserUpdateData user)
        {
            var existingUser = await _myData.users.FirstOrDefaultAsync(x => x.Id == id);

            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Gender= user.Gender;
                existingUser.Address= user.Address;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Role = AddUserRole(user.Password);
                await _myData.SaveChangesAsync();

                return "User is Updated";
            }
            return "User does not exist";

        }
        public async Task<string> DeleteUserInDB(int id)
        {
          var user = await _myData.users.SingleAsync(x=> x.Id == id);
            if (user == null)
            {
                return "User does not exist";
            }
            _myData.users.Remove(user);
            await _myData.SaveChangesAsync();
            return "User is Deleted";
        }

        public async Task<User> UserValidation(LoginData loginData)
        {
            var data = await _myData.users.FirstOrDefaultAsync(x => x.Email == loginData.Email && x.Password == loginData.Password);


            return data;
        }
    }
}
