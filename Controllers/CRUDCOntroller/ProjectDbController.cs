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
        AddProject = 0, EditProject, RemoveProject, GetAllProject, GetProjectById
    }

    public class ProjectDbController : ControllerBase<UserControllerOperations>, ICRUDControllerAsync<User>
    {
        #region Fields

        PMDBContext m_db;

        CancellationTokenSource m_cancellationTokenSource;

        #endregion

        #region Properties

        public CancellationTokenSource CancellationTokenSource
        {
            get => m_cancellationTokenSource;
            set => m_cancellationTokenSource = value;
        }

        #endregion

        #region Ctor

        public ProjectDbController(PMDBContext dbcontext, CancellationTokenSource cts)
        {
            m_db = dbcontext;

            m_cancellationTokenSource = cts;
        }

        #endregion

        #region Methods

        public async Task AddAsync(User entity)
        {
            if (entity != null)
                await ExecuteAndGetResultViaEventAsync(UserControllerOperations.AddProject,
                    (token, state) =>
                    {
                        m_db.UsersTable.Add(entity);

                        return m_db.SaveChanges();

                    }, null, m_cancellationTokenSource.Token
                    );
        }

        public async Task GetAsync(Guid id)
        {
            if (id != default)
                await ExecuteAndGetResultViaEventAsync(UserControllerOperations.GetProjectById,
                    (oken, state) =>
                    {
                        return (from u in m_db.UsersTable where u.Id == id select u).First() ?? null;
                    }, null, m_cancellationTokenSource.Token);
        }

        public async Task GetAllAsync()
        {
            await ExecuteAndGetResultViaEventAsync(UserControllerOperations.GetAllProject,
                    (oken, state) =>
                    {
                        return m_db.UsersTable.ToList();
                    }, null, m_cancellationTokenSource.Token);
        }

        public async Task RemoveAsync(Guid id)
        {
            await ExecuteAndGetResultViaEventAsync(UserControllerOperations.RemoveProject,
                    (oken, state) =>
                    {
                        var user = (from u in m_db.UsersTable where u.Id == id select u).First() ?? null;

                        int rows = -1;

                        if (user != null)
                        {
                            m_db.UsersTable.Remove(user);

                            rows = m_db.SaveChanges();
                        }
                        return rows;

                    }, null, m_cancellationTokenSource.Token);
        }

        public async Task UpdateAsync(Guid id, User newEntity, int bitmask)
        {
            await ExecuteAndGetResultViaEventAsync(UserControllerOperations.RemoveProject,
                    (oken, state) =>
                    {
                        var user = (from u in m_db.UsersTable where u.Id == id select u).First() ?? null;

                        int rows = -1;

                        if (user != null)
                        {
                            //Update system



                            rows = m_db.SaveChanges();
                        }
                        return rows;

                    }, null, m_cancellationTokenSource.Token);
        }

        public async Task<Guid> GenerateIdAsync()
        {
            Guid temp = Guid.NewGuid();

            await Task.Run(
                () =>
                {
                    while ((m_db.UsersTable.Where(u => u.Id.Equals(temp))).Count() > 0)
                    {
                        temp = Guid.NewGuid();
                    }
                }
                );

            return temp;
        }



        #endregion


    }
}
