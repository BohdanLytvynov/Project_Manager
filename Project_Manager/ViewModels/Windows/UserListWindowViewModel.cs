using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.ViewModelBase;

namespace Project_Manager.ViewModels.Windows
{
    public class UserListWindowViewModel : ViewModelBase
    {
        #region Fields

        int m_usersCount;

        #endregion

        #region Properties
        public int UsersCount 
        {
            get=> m_usersCount; 
            
            set=> SetProperty(ref m_usersCount, value, nameof(UsersCount));
        }
        #endregion

        #region Ctor
        public UserListWindowViewModel()
        {
            #region Init fields

            m_usersCount = 0;

            #endregion
        }
        #endregion

        #region Methods

        #endregion
    }
}
