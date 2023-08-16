using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using Project_Manager.ViewModels;
using ViewModelBaseLib.Commands;
using System.Windows.Forms;
using System.Windows;

namespace Project_Manager.ViewModels
{
    class SettingsWindowViewModel : ViewModelCommon
    {
        #region Fields
        string pathtodb; //path to db and archive

        private Window thiswindow;// Current window instance

        FolderBrowserDialog _folderdialog; //folder browser dialog instance
        #endregion

        #region Properties
        public string PathToDb
        {
            get => pathtodb;

            set => SetProperty<string>(ref pathtodb, value, nameof(PathToDb));
        }
        #endregion

        #region Command Properties
        public ICommand OnOpenDialogePressed { get; }

        public ICommand OnAcceptButtonPressed { get; }

        
        #endregion

        public SettingsWindowViewModel(Window thisWindow)
        {
            thiswindow = thisWindow;

            PathToDb = Path;

            OnAcceptButtonPressed = new Command(
                CanOnAcceptButtonPressed,
                OnAcceptButtonPressedExecute
                );

            OnOpenDialogePressed = new Command(
                CanOpensDialogeExecuted,
                OnOpensDialogExecute
                );
        }

        #region Methods
        #region On Open Dialoge Pressed
        private void OnOpensDialogExecute(object p)
        {
            using (_folderdialog = new FolderBrowserDialog())
            {
                DialogResult result = _folderdialog.ShowDialog();

                if (result == DialogResult.OK)
                    PathToDb = _folderdialog.SelectedPath;

            }

        }

        private bool CanOpensDialogeExecuted(object p) => true;
        #endregion

        #region On Accept Button Pressed
        private bool CanOnAcceptButtonPressed(object p)
        {
            return true;            
        }

        private void OnAcceptButtonPressedExecute(object p)
        {
            if (Path.Length != PathToDb.Length)
            {
                Path = PathToDb + @"\DataBase.json";

                PathToArchive = PathToDb + @"\Archive.json";
            }  
            thiswindow.Close();
        }
        #endregion
        #endregion
    }
}
