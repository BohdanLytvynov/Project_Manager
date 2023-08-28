using ControllerLib_DotNetFramework;
using Controllers.CRUDCOntroller.Inerfaces;
using Data;
using Data.DBContexts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controllers.CRUDController
{
    public enum UserControllerOperations
    { 
        AddUser = 0, EditUser, RemoveUser, GetAllUsers, GetUserById
    }

    public class UserDBCRUDController : ControllerBase<UserControllerOperations>, ICRUDControllerAsync<User>
    {
        #region Fields

        PMDBContext m_db;

        CancellationTokenSource m_cancellationTokenSource;

        #endregion

        #region Ctor

        public UserDBCRUDController(PMDBContext dbcontext, CancellationTokenSource cts)
        {
            m_db = dbcontext;

            m_cancellationTokenSource = cts;
        }

        #endregion

        #region Methods

        public async Task AddAsync(User entity)
        {
            if(entity != null)
                await ExecuteAndGetResultViaEventAsync(UserControllerOperations.AddUser,
                    (token, state)=>
                    {
                        m_db.UsersTable.Add(entity);

                        m_db.SaveChanges();

                        return null;

                    }, null, m_cancellationTokenSource.Token
                    );

           
        }

        public Task GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, User newEntity)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
