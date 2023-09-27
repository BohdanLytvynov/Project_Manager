using Controllers.CRUDController;
using Controllers.Managers;
using CustomControlsLibrary.Extentions;
using Data.DBContexts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModelBaseLib.Commands;
using ViewModelBaseLib.ViewModelBase;

namespace Project_Manager.ViewModels.Windows
{
    public class AddUserViewModel : ViewModelBase
    {
        #region Fields

        UserManager m_userManager;

        string m_login;

        SecureString m_password;

        string m_keyWord;

        Func<int, SecureString, TextBlock, bool> m_checkPassword;

        CancellationTokenSource m_cts;

        #endregion

        #region Properties

        public string Login { get=> m_login; set=> SetProperty(ref m_login, value, nameof(Login)); }

        public string KeyWord { get=> m_keyWord; set=> SetProperty(ref m_keyWord, value, nameof(KeyWord)); }

        public Func<int, SecureString, TextBlock, bool> CheckPasswords 
        { get=>m_checkPassword; set=> SetProperty(ref m_checkPassword, value, nameof(CheckPasswords)); }

        #endregion

        #region IDataErrorInfo

        public override string this[string columnName]
        {
            get 
            {
                string error = String.Empty;

                switch (columnName)
                {
                    case nameof(Login)://Login field check

                        if (String.IsNullOrEmpty(Login))
                        {
                            m_ValidArray[0] = false;

                            error = "Пустое поле!";
                        }
                        else if(m_userManager.IsLoginExists(Login))
                        {
                            m_ValidArray[0] = false;

                            error = "Такой ЛОГИН уже существует!";
                        }

                        m_ValidArray[0] = true;

                        break;

                        //1,2 Password Boxes Checking

                    case nameof(KeyWord)://Key Word Field

                        m_ValidArray[3] = !String.IsNullOrEmpty(KeyWord);

                        if (!m_ValidArray[3])
                        {
                            error = "Поле не должно быть пустым!";
                        }

                        break;
                }

                return error;
            }
        }

        #endregion

        #region Commands

        public ICommand OnAddUserButtonPressed { get; }

        #endregion

        #region Ctor
        public AddUserViewModel(DbConnectionStringBuilder conStringBuilder)
        {
            #region Init Fields

            m_cts = new CancellationTokenSource();

            m_userManager = new UserManager(new PMDBContext(conStringBuilder.ConnectionString), m_cts);

            m_login = String.Empty;

            m_keyWord = String.Empty;
            
            m_checkPassword = new Func<int, SecureString, TextBlock, bool>(CheckPassword);
                       
            m_ValidArray = new bool[4];

            #endregion

            #region Init Commands

            OnAddUserButtonPressed = new Command(
                CanOnAddUserButtonPressedExecute,
                OnAddUserButtonPressedExecute
                );

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

                    correct = arg2.Length > 0? true: false;

                    if (!correct)                    
                        arg3.Text = "Поле не должно быть пустым!";
                    else
                        m_password = arg2;

                    m_ValidArray[1] = correct;

                    break;

                case 1:

                    bool Pass2Filled = arg2.Length > 0;

                    bool PassEqual = arg2.PassEquals(m_password);

                    correct = Pass2Filled && PassEqual;

                    m_ValidArray[2] = correct;

                    if (!Pass2Filled)
                        arg3.Text = "Поле не должно быть пустым!";

                    if (!PassEqual)
                        arg3.Text = "Пароли не совпадают!";

                    break;
            }

            return correct;
        }

        #endregion

        #region On Add User Button Pressed Execute

        private bool CanOnAddUserButtonPressedExecute(object p)
        {
            //Debug.WriteLine("In Can Method");
            //int i = 0;
            //foreach (var item in m_ValidArray)
            //{
            //    Debug.WriteLine($"Valid {i}: {item}");
            //}

            return CheckValidArray(0, 3);
        }

        private async void OnAddUserButtonPressedExecute(object p)
        {
            //Guid id = await m_userDbController.GenerateId();

            //await m_userDbController.AddAsync(
            //    new User
            //    (
            //        id, Login,     
            //    )
            //    );
        }

        #endregion

        #endregion
    }
}
