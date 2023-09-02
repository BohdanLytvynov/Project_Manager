using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ViewModelBaseLib.ViewModelBase;

namespace Project_Manager.ViewModels.Windows
{
    public class AddUserViewModel : ViewModelBase
    {
        #region Fields

        byte m_passBox;

        string m_login;

        SecureString[] m_passwords;

        string m_keyWord;

        Func<int, SecureString, TextBlock, bool> m_checkPassword;

        #endregion

        #region Properties

        public string Login { get=> m_login; set=> SetProperty(ref m_login, value, nameof(Login)); }

        public string KeyWord { get=> m_keyWord; set=> SetProperty(ref m_keyWord, value, nameof(KeyWord)); }

        public Func<int, SecureString, TextBlock, bool> CheckPasswords 
        { get=>m_checkPassword; set=> SetProperty(ref m_checkPassword, value, nameof(CheckPasswords)); }

        #endregion

        #region Ctor
        public AddUserViewModel()
        {
            #region Init Fields

            m_login = String.Empty;

            m_keyWord = String.Empty;

            m_passwords = new SecureString[2];

            m_checkPassword = new Func<int, SecureString, TextBlock, bool>(CheckPassword);
           
            m_passBox = 0;

            #endregion
        }

        #endregion

        #region Methods

        #region Check Passwords

        private bool CheckPassword(int arg1, SecureString arg2, TextBlock arg3)
        {
            bool correct = true;

            switch (arg1)
            {
                case 0:



                    break;

                case 1:
                    break;
            }

            return correct;
        }

        #endregion

        #endregion
    }
}
