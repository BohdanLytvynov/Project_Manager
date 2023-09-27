using ControllerLib_DotNetFramework.Interfaces.Controller;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Managers.Interfaces
{
    public interface IUserManager<Entity> 
        where Entity : User
    {
        Task<bool> LoginAsync(string Login, SecureString pass);

        Task RegisterAsync(Entity user);

        bool IsLoginExists(string login);

        Task GetAllUsersAsync();
    }
}
