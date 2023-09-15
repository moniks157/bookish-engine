﻿using Shelfie.Repository.Models;

namespace Shelfie.Repository.Repositories.Interfaces;

public interface IUserRepository
{
    User? GetUser(string username);
    User? GetUser(int id);
    User? CreateUser(User user);
}
