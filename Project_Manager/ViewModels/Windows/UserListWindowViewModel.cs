using ControllerLib_DotNetFramework.Enums;
using ControllerLib_DotNetFramework.Interfaces.Controller;
using Controllers.CRUDController;
using Data.DBContexts;
using Data.Models;
using ModelsLib.Models;
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
        #region Fields

        int m_usersCount;

        ObservableCollection<UserVM> m_users;

        UserDBCRUDController m_userDBCRUDController;

        CancellationTokenSource m_cts;

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

            m_usersCount = 0;

            m_users = new ObservableCollection<UserVM>();

            m_cts = new CancellationTokenSource();

            m_userDBCRUDController = new UserDBCRUDController(
                new PMDBContext(conStrBuilder.ConnectionString), m_cts
                );

            m_userDBCRUDController.OnOperationCompleted += M_userDBCRUDController_OnOperationCompleted;

            #endregion

            #region Init Commands

            OnAddUserButtonPressed = new Command(
                CanOnAddUserButtonPressedExecute,
                OnAddUserButtonPressedExecute
                );

            #endregion

            m_userDBCRUDController.GetAllAsync();
        }

        private void M_userDBCRUDController_OnOperationCompleted(IOperationResult<UserControllerOperations> obj)
        {
            if (obj.Success && obj.ExecutionState == ExecutionState.Finished)
            {
                switch (obj.Name)
                {
                    case UserControllerOperations.AddUser:
                        break;
                    case UserControllerOperations.EditUser:
                        break;
                    case UserControllerOperations.RemoveUser:
                        break;
                    case UserControllerOperations.GetAllUsers:

                        if (Users.Count > 0)
                            Users.Clear();

                        var users = obj.Result as List<User>;

                        if (users != null && users.Count > 0)
                        {
                            foreach (var u in users)
                            {
                                Users.Add(
                                    new UserVM(
                                        u.Id, u.Login
                                        )
                                    );
                            }
                        }

                        break;
                    case UserControllerOperations.GetUserById:
                        break;
                    default:
                        break;
                }
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
            
        }
        #endregion

        #endregion
    }
}
