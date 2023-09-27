using ControllerLib_DotNetFramework;
using ControllerLib_DotNetFramework.Interfaces.Controller;
using Controllers.Generators.Interfaces;
using Controllers.Managers.Interfaces;
using Data.DBContexts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controllers.Managers
{
    public enum UserManagerOperations
    {
        Login = 0, Register, IsLoginExists, GetAllUsers
    }

    public class UserManager : ControllerBase<UserManagerOperations>, IUserManager<User>,
        IGenerateId<PMDBContext>
    {
        #region Fields

        private PMDBContext m_db;

        private CancellationTokenSource m_cts;

        #endregion

        #region Ctor
        public UserManager(PMDBContext dataBase, CancellationTokenSource cts)
        {
            m_db = dataBase;

            m_cts = cts;
        }

        public Guid Generate(PMDBContext dbContext)
        {
            Guid id = Guid.NewGuid();

            while ((m_db.UsersTable.Where(u => u.Id.Equals(id)).Count() > 0))
            {
                id = Guid.NewGuid();
            }

            return id;
        }

        public async Task GetAllUsersAsync()
        {
            await ExecuteAndGetResultViaEventAsync(
                UserManagerOperations.GetAllUsers,
                (cts, state) =>
                {
                    return (from u in m_db.UsersTable select u).OrderBy(u => u.Login).ToList();
                }
                , null, m_cts.Token);
        }

        public bool IsLoginExists(string login)
        {
            if(login == null)
                return false;
            if(login.Length == 0)
                return false;

            return (from u in m_db.UsersTable where u.Login.Equals(login) select u).Count() > 0;
        }




        #endregion

        #region Methods

        public async Task<bool> LoginAsync(string Login, SecureString pass)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(User user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
