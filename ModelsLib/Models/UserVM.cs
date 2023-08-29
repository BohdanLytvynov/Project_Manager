using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.ViewModelBase;

namespace ModelsLib.Models
{
    public class UserVM : ViewModelBase
    {
        #region Fields

        #region Id

        Guid m_id;

        #endregion

        string m_login;

        string m_keyWord;

        SecureString m_password;

        #endregion

        #region Properties
        public string Login 
        {
            get=> m_login;
            
            set=> SetProperty(ref m_login, value, nameof(Login));
        }
        #endregion

        #region Ctor

        public UserVM():this(default, String.Empty)
        {
            
        }

        public UserVM(Guid id, string login)
        {
            m_id = id;

            m_login = login;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Login}";
        }
        #endregion
    }
}
