﻿using DTO;
using Entities.Models;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        int CheckPassword(string password);
        //User GetUserById(int id);
        Task<User> Login(string Password, string UserName);
        Task<User> UpdateUser(int id, User userToUpdate);
        // בהצלחה
    }
}