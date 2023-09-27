using ControllerLib_DotNetFramework.Enums;
using ControllerLib_DotNetFramework.Interfaces.Controller;
using Controllers.CRUDController;
using Controllers.Managers;
using Data.DBContexts;
using Data.Models;
using ModelsLib.Models;
using Project_Manager.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelBaseLib.Commands;
using ViewModelBaseLib.ViewModelBase;

namespace Project_Manager.ViewModels.Windows
{
    public class UserListWindowViewModel : ViewModelBase
    {

        #region Windows

        AddUser m_addUserWindow;

        #endregion

        #region Fields

        int m_usersCount;

        ObservableCollection<UserVM> m_users;

        UserManager m_um;

        CancellationTokenSource m_cts;

        DbConnectionStringBuilder m_constrBuilder;

        #endregion

        #region Properties

        public ObservableCollection<UserVM> Users { get=>m_users ; set=> m_users = value; }

        public int UsersCount 
        {
            get=> m_usersCount; 
            
            set=> SetProperty(ref m_usersCount, value, nameof(UsersCount));
        }


        #endregion

        #region Commands

        public ICommand OnAddUserButtonPressed { get;}

        public ICommand OnEditUserButtonPressed { get; }

        public ICommand OnRemoveUserButtonPressed { get; }

        public ICommand OnSelectUserButtonPressed { get; }

        #endregion

        #region Ctor
        public UserListWindowViewModel(DbConnectionStringBuilder conStrBuilder)
        {
            #region Init fields

            m_constrBuilder = conStrBuilder;

            m_usersCount = 0;

            m_users = new ObservableCollection<UserVM>();

            m_cts = new CancellationTokenSource();

            m_um = new UserManager(new PMDBContext(conStrBuilder.ConnectionString), m_cts);

            m_um.OnOperationCompleted += M_um_OnOperationCompleted;

            #endregion

            #region Init Commands

            OnAddUserButtonPressed = new Command(
                CanOnAddUserButtonPressedExecute,
                OnAddUserButtonPressedExecute
                );

            #endregion

            m_um.GetAllUsersAsync();
            
        }

        private void M_um_OnOperationCompleted(IOperationResult<UserManagerOperations> obj)
        {
            if (obj == null)
                return;

            if (obj.ExecutionState == ExecutionState.Finished)
            {
                switch (obj.Name)
                {                                      
                    case UserManagerOperations.GetAllUsers:

                        if (Users.Count > 0)
                            Users.Clear();

                        foreach (User user in obj.Result)
                        {
                            Users.Add(new UserVM(user.Id, user.Login));
                        }

                        break;
                   
                }
            }
            else if (obj.ExecutionState == ExecutionState.Canceled)
            { 
            
            }
            else
            {

            }

        }


        #endregion

        #region Methods

        #region On Add User Button Pressed

        private bool CanOnAddUserButtonPressedExecute(object p) => true;

        private void OnAddUserButtonPressedExecute(object p)
        {
            m_addUserWindow = new AddUser(m_constrBuilder);

            m_addUserWindow.Topmost = true;

            m_addUserWindow.ShowDialog();
        }
        #endregion

        #endregion
    }
}
