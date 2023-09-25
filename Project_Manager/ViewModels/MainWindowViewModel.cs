using System;
using ModelsLib.Models;
using Project_Manager;
using Storage;
using ViewModelBaseLib.Commands;
using System.Windows.Input;
using Project_Manager.Views;
using System.Windows;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DispatcherJsonSerialization;
using System.Windows.Data;
using System.Data.Common;
using Data;
using Project_Manager.Views.Windows;
using Data.DBContexts;

namespace Project_Manager.ViewModels
{
    enum CurrentColumn 
    { 
        NewProjects,
        ProjectsInWork,
        ProjectsWaiting,
        ProjectsEstablished,
        ProjectsPayed
    }

    public class MainWindowViewModel: ViewModelCommon
    {
        #region Windows
        
        #endregion

        #region Fields

        private DbConnectionStringBuilder m_csbuild;

        private string m_pathToExe;

        CurrentColumn TheCurrentColumn; //The current column where user is now
        
        int selectednewprojectindex = -1; //index of selected project in a new projects column

        int selectedProjectsInWorkindex = -1; //index of selected project in a projects in work column

        int selectedprojectswaitingindex = -1; //index of selected project in a projects waiting column

        int selectedprojectsestablishedindex = -1; //index of selected project in a projects established column

        int selectedpaidprojectsindex = -1; //index of selected project in a projects paid column
        #endregion

        #region Properties
        public int SelectedNewProjectIndex
        {
            get => selectednewprojectindex;

            set 
            { 
                SetProperty<int>(ref selectednewprojectindex, value, nameof(SelectedNewProjectIndex));

                TheCurrentColumn = CurrentColumn.NewProjects;
            }
        }

        public int SelectedProjectsinWorkIndex
        {
            get => selectedProjectsInWorkindex;

            set 
            { 
                SetProperty<int>(ref selectedProjectsInWorkindex, value, nameof(SelectedProjectsinWorkIndex));

                TheCurrentColumn = CurrentColumn.ProjectsInWork;
            }
        }

        public int SelectedWaitingProjectsIndex
        {
            get => selectedprojectswaitingindex;

            set
            {
                SetProperty<int>(ref selectedprojectswaitingindex, value, nameof(SelectedWaitingProjectsIndex));

                TheCurrentColumn = CurrentColumn.ProjectsWaiting;
            }
        }

        public int SelectedEstablishedProjectsIndex
        {
            get => selectedprojectsestablishedindex;

            set 
            { 
                SetProperty<int>(ref selectedprojectsestablishedindex, value, nameof(SelectedEstablishedProjectsIndex));

                TheCurrentColumn = CurrentColumn.ProjectsEstablished;
            }
        }

        public int SelectedPaidprojectIndex
        {
            get => selectedpaidprojectsindex;

            set
            {
                SetProperty<int>(ref selectedpaidprojectsindex, value, nameof(SelectedPaidprojectIndex));

                TheCurrentColumn = CurrentColumn.ProjectsPayed;
            }
        }
        #endregion

        #region Path to Settings
        private static string pathtoconfig = Environment.CurrentDirectory + @"\Settings.json";        
        #endregion
        
        #region Commands
        public ICommand OnSettingsButtonPressed { get; }

        public ICommand OnAddProjectButtonPressed { get; }

        public ICommand OnMoveProjectsRightPressed { get; }

        public ICommand OnMoveProjectsLeftPressed { get; }

        public ICommand OnRemoveButtonPressed { get; }

        public ICommand OnStatisticsButtonPressed { get; }

        public ICommand OnUsersButtonPressed { get; }
        #endregion

        static MainWindowViewModel()
        {
            //DeserializeSettings();
            
            //Application.Current.MainWindow.Closing += new CancelEventHandler(OnAppClosing);
        }

        public MainWindowViewModel(Window w)
        {
            #region Initialization

            //Calculate path to DB

            m_pathToExe = Environment.CurrentDirectory;

            string pathToDB = m_pathToExe + @"\" + "DataBase" + @"\" + "PMDB.mdf";

            //ConnectToLocalDB

            m_csbuild = new DbConnectionStringBuilder();

            m_csbuild.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                $"AttachDbFilename={pathToDB};" +
                "Integrated Security=True;Connect Timeout=30";

            PMDBContext pmdb = new PMDBContext(m_csbuild.ConnectionString);

            #endregion

            #region InitFields

            pu = new ProjectUrgency();

            collectionsdb = new CollectionsDB();

            archive = new Archive();

            #endregion

            #region Init Fields
            
            #endregion

            #region Init Commands

            OnSettingsButtonPressed = new Command(
                CanOnSettingsButtonPressedExecute,
                OnSettingsButtonPressedExecute
                );

            OnAddProjectButtonPressed = new Command(
                CanOnAddProjectButtonPressed,
                OnAddProjectButtonPressedExecute
                );
            OnMoveProjectsRightPressed = new Command(
                CanMoveRight,
                OnMoveRight
                );

            OnMoveProjectsLeftPressed = new Command(
                CanMoveLeft,
                MoveLeft
                );

            OnRemoveButtonPressed = new Command(
                CanRemoveButtonPressedExecute,
                OnRemoveButtonPressedExecute
                );

            OnStatisticsButtonPressed = new Command(
                CanStatisticButtonPressedExecute,
                OnStatisticButtonPressedExecute
                );

            OnUsersButtonPressed = new Command(
                CanOnUsersButtonPressedExecute,
                OnUsersButtonPressedExecute
                );

            #endregion
        }
       
        #region Methods
        #region On Settings Button pressed Execute
        private bool CanOnSettingsButtonPressedExecute(object p) => true;

        private void OnSettingsButtonPressedExecute(object p)
        {
            ShowDialogWindow();
        }

        private void ShowDialogWindow()
        {
            SettingsWindow window = new SettingsWindow();

            window.Topmost = true;

            window.ShowDialog();
        }
        #endregion

        #region On Add Project Button Pressed
        private bool CanOnAddProjectButtonPressed(object p) =>
            true;

        private void OnAddProjectButtonPressedExecute(object p)
        {
            collectionsdb.NewProjects.Add(
                new Project("Новый проект", "Описание", DateTime.Now, "Введите цену", pu[2], "Заказчик")
                );

            MergeSort(collectionsdb.NewProjects, 0, collectionsdb.NewProjects.Count - 1);
        }

        #endregion

        #region On App Closing

        private static void OnAppClosing(object s, CancelEventArgs e)
        {            
            //SaveCollections();                                             
        }

        private static void SaveCollections()
        {            
            DispatcherJsonSerializer.SerializeWithDispatcher<CollectionsDB>(settings.PathToDataBase, collectionsdb);
            
            SerializeSettings();            
        }

        private static void SerializeSettings()
        {
            
        }

        private static void DeserializeSettings()
        {
            

            FileInfo file = new FileInfo(pathtoconfig);

            if (file.Length <= 2 || file.Length == 0)
                settings = new Settings();
            else
                settings = new Settings();
        }

        #endregion

        #region DeserializeIfDbisNotEmpty
        private T DeserializeIfDbIsNotEmpty<T>(string path, T Object)
            where T:new()
        {
            FileInfo file = new FileInfo(path);
            if (file.Length <= 2 || file.Length == 0)
                return new T();
            else
                return new T();
        }
        #endregion

        #region Move Projects right
        private bool CanMoveRight(object p) => true;                   

        private void OnMoveRight(object p)
        {
            try
            {
                switch (TheCurrentColumn)
                {
                    case CurrentColumn.NewProjects:
                        MoveProject(collectionsdb.ProjectsInWork, collectionsdb.NewProjects,
                            SelectedNewProjectIndex);                       
                        break;
                    case CurrentColumn.ProjectsInWork:
                        MoveProject(collectionsdb.ProjectsWaiting, collectionsdb.ProjectsInWork,
                            SelectedProjectsinWorkIndex);
                        break;
                    case CurrentColumn.ProjectsWaiting:
                        MoveProject(collectionsdb.ProjectsEstablished, collectionsdb.ProjectsWaiting
                            ,SelectedWaitingProjectsIndex);
                        break;
                    case CurrentColumn.ProjectsEstablished:
                        MoveProject(collectionsdb.ProjectsPayed, collectionsdb.ProjectsEstablished,
                            SelectedEstablishedProjectsIndex);
                        break;
                    case CurrentColumn.ProjectsPayed:

                        archive.ArchiveProp.Add(CreateArchiveNote(collectionsdb.ProjectsPayed
                            [SelectedPaidprojectIndex]));

                        archive.CommonSum += collectionsdb.ProjectsPayed[SelectedPaidprojectIndex].Cost;

                        collectionsdb.ProjectsPayed.RemoveAt(SelectedPaidprojectIndex);

                        break;
                    default:
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                CommonFunctions.MessageBoxCreate("Не выделен не один проект!", 
                    MessageBoxButton.OK, MessageBoxImage.Error);              
            }
            
        }
        #endregion

        #region Move Projects left
        private bool CanMoveLeft(object p) => true;

        private void MoveLeft(object p)
        {
            try
            {
                switch (TheCurrentColumn)
                {
                    case CurrentColumn.ProjectsWaiting:
                        MoveProject(collectionsdb.ProjectsInWork, collectionsdb.ProjectsWaiting,
                            SelectedWaitingProjectsIndex);
                        break;

                    default:
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                CommonFunctions.MessageBoxCreate("Не выделен не один проект!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        /// <summary>
        /// Move Project from 1 collection to another
        /// </summary>
        /// <param name="collectionresiver"></param>
        /// <param name="collectionsender"></param>
        /// <param name="projectindex"></param>
        private void MoveProject(ObservableCollection<Project> collectionresiver,
           ObservableCollection<Project> collectionsender, int projectindex)
        {
            collectionresiver.Add(collectionsender[projectindex]);
            
            collectionsender.RemoveAt(projectindex);

            MergeSort(collectionresiver, 0, collectionresiver.Count - 1);
        }

        /// <summary>
        /// Remove selected project
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="indexforremoving"></param>
        private void RemoveProject(ObservableCollection<Project> collection, int indexforremoving)
        {
            try
            {
                collection.RemoveAt(indexforremoving);
            }
            catch (ArgumentOutOfRangeException)
            {
                CommonFunctions.MessageBoxCreate("Не выделен не один проект!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        #region On Remove Pressed

        private bool CanRemoveButtonPressedExecute(object p)
        {
            return true;
        }

        private void OnRemoveButtonPressedExecute(object p)
        {
            switch (TheCurrentColumn)
            {
                case CurrentColumn.NewProjects:
                    RemoveProject(collectionsdb.NewProjects, SelectedNewProjectIndex);
                    break;
                case CurrentColumn.ProjectsInWork:
                    RemoveProject(collectionsdb.ProjectsInWork, SelectedProjectsinWorkIndex);
                    break;
                case CurrentColumn.ProjectsWaiting:
                    RemoveProject(collectionsdb.ProjectsWaiting, SelectedWaitingProjectsIndex);
                    break;
                case CurrentColumn.ProjectsEstablished:
                    RemoveProject(collectionsdb.ProjectsEstablished, SelectedEstablishedProjectsIndex);
                    break;
                case CurrentColumn.ProjectsPayed:

                    archive.ArchiveProp.Add(CreateArchiveNote(collectionsdb.ProjectsPayed
                            [SelectedPaidprojectIndex]));

                    archive.CommonSum += collectionsdb.ProjectsPayed[SelectedPaidprojectIndex].Cost;

                    collectionsdb.ProjectsPayed.RemoveAt(SelectedPaidprojectIndex);

                    RemoveProject(collectionsdb.ProjectsPayed, SelectedPaidprojectIndex);

                    break;
                default:
                    break;
            }
        }

        #endregion

        #region On Statistics Button Pressed
        private bool CanStatisticButtonPressedExecute(object p)
        {
            return true;
        }

        private void OnStatisticButtonPressedExecute(object p)
        {
            Statistics statisticwindow = new Statistics();

            statisticwindow.Topmost = true;

            statisticwindow.Show();
        }

        #endregion

        #region On Users Button Pressed

        private bool CanOnUsersButtonPressedExecute(object p) => true;

        private void OnUsersButtonPressedExecute(object p)
        {
            UserListWindow usersWindow = new UserListWindow(m_csbuild);

            usersWindow.Topmost = true;

            usersWindow.Show();            
        }

        #endregion

        /// <summary>
        /// Sets the field istheend in archive note to its propriate value
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool SetTheEndOfMonth(DateTime date)
        {
            if (date.Day < 28 && date.Day > 31) //Не дошли до конца месяца
            {
                return false;
            }
            else //Дошли до конца
            {
                var daysamount = DateTime.DaysInMonth(date.Year, date.Month);

                if (daysamount == date.Day)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Creates archive note according to current's project info
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private ArchiveNote CreateArchiveNote(Project current)
        {
            var startDate = current.Date;
            var endDate = DateTime.Now;
            var timespan = endDate - startDate;

            return new ArchiveNote(
                            collectionsdb.ProjectsPayed[SelectedPaidprojectIndex].Name,
                            collectionsdb.ProjectsPayed[SelectedPaidprojectIndex].Cost,
                            timespan, endDate, SetTheEndOfMonth(endDate)
                            );
        }


        private void MergeSort(ObservableCollection<Project> collection, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                
                MergeSort(collection, left, middle);

                MergeSort(collection, middle + 1, right);

                MainMerge(collection, left, middle, right);
            }
        }

        private void MainMerge(ObservableCollection<Project> collection, int left, int middle, int right)
        {
            //int n1 = middle - left + 1;
            //int n2 = right - middle;

            //ObservableCollection<Project> L = new ObservableCollection<Project>();

            //ObservableCollection<Project> R = new ObservableCollection<Project>();

            //int i, j;

            //for (i = 0; i < n1; i++)
            //{
            //    L.Add(collection[left + i]);
            //}

            //for (j = 0; j < n2; j++)
            //{
            //    R.Add(collection[middle + 1 + j]);
            //}

            //i = 0; j = 0;

            //int k = left;

            //while (i< n1 && j<n2)
            //{
            //    if (L[i] > R[j] || L[i] == R[j])
            //    {
            //        collection[k] = L[i];
            //        i++;
            //    }
            //    else
            //    {
            //        collection[k] = R[j];
            //        j++;
            //    }
            //    k++;

            //    while (i<n1)
            //    {
            //        collection[k] = L[i];
            //        i++;
            //        k++;
            //    }

            //    while (j<n2)
            //    {
            //        collection[k] = R[j];
            //        j++;
            //        k++;
            //    }
            //}
        }
        #endregion
    }
}
