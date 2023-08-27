using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage;
using System.Collections.ObjectModel;
using ModelsLib.Models;
using ViewModelBaseLib.ViewModelBase;
using ViewModelBaseLib.Commands;
using System.Windows.Input;
using System.Windows;
using System.Diagnostics;

namespace Project_Manager.ViewModels
{
    class StatisticsViewModel : ViewModelCommon
    {
        #region Fields
        private DateTime m_start; //serach start date

        private Window current; //current window instance

        private DateTime m_end; //serach end date

        private ObservableCollection<ArchiveNote> m_searchresult; //Search result

        private ObservableCollection<DateTime> monthCollection; //month collection

        private ObservableCollection<double> countCollection; //project count per month

        private ObservableCollection<double> revenueCollection; //revenue per month

        private ObservableCollection<double> middlerevenue; //average revenue per month

        private double Maxamountofdays; //max amount of days spent for the project

        private double Minamountofdays; //min amount of days spent for the project

        private DateTime Maxamountofdaysdate; //the month with a max amount of days spent for the project

        private DateTime Minamountofdaysdate; //the month with a min amount of days spent for the project

        private string maxprojname; //Name of the project which max amount of days was spent on

        private string minprojectname; //Name of the project which min amount of days was spent on

        #endregion

        #region Properties

        public ObservableCollection<double> MiddleRevenueCollection
        {
            get => middlerevenue;

            set => middlerevenue = value;
        }

        public string MaxProjectName
        {
            get => maxprojname;

            set => SetProperty<string>(ref maxprojname, value, nameof(MaxProjectName));
        }

        public string MinProjectName
        {
            get => minprojectname;

            set => SetProperty<string>(ref minprojectname, value, nameof(MinProjectName));
        }

        public double MaxAmountOfDays
        {
            get => Maxamountofdays;

            set => SetProperty<double>(ref Maxamountofdays, value, nameof(MaxAmountOfDays));
        }

        public double MinAmountOfDays
        {
            get => Minamountofdays;

            set => SetProperty<double>(ref Minamountofdays, value, nameof(MinAmountOfDays));
        }

        public DateTime MinAmountOfDaysDate
        {
            get => Minamountofdaysdate;

            set => SetProperty<DateTime>(ref Minamountofdaysdate, value, nameof(MinAmountOfDaysDate));
        }

        public DateTime MaxAmountOfDaysDate
        {
            get => Maxamountofdaysdate;

            set => SetProperty<DateTime>(ref Maxamountofdaysdate, value, nameof(MaxAmountOfDaysDate));
        }

        public ObservableCollection<double> RevenueCollectionProp
        {
            get => revenueCollection;
            set
            {
                revenueCollection = value;
                
            }
        }

        public ObservableCollection<double> CountCollectionProp
        {
            get => countCollection;
            set
            {
                countCollection = value;
                
            }
        }


        public ObservableCollection<DateTime> MonthCollectionProp
        {
            get => monthCollection;

            set 
            { 
                monthCollection = value;
                OnPropertyChanged(nameof(MonthCollectionProp));
            }
        }

        public DateTime Start
        {
            get => m_start;

            set => SetProperty<DateTime>(ref m_start, value, nameof(Start));
        }

        public DateTime End
        {
            get => m_end;

            set => SetProperty<DateTime>(ref m_end, value, nameof(End));
        }

        public ObservableCollection<ArchiveNote> SearchResult
        {
            get => m_searchresult;

            set => m_searchresult = value;
        }

        public Archive ArchiveCollection
        {
            get => archive;            
        }

        #region Commands

        public ICommand OnSearchButtonPressed { get; }

        public ICommand OnBuildGraphicsPressed { get; }

        #endregion

        #endregion

        #region Constructor

        public StatisticsViewModel(Window w)
        {            
            current = w;

            m_searchresult = new ObservableCollection<ArchiveNote>();
            
            monthCollection = new ObservableCollection<DateTime>();

            countCollection = new ObservableCollection<double>();

            revenueCollection = new ObservableCollection<double>();

            middlerevenue = new ObservableCollection<double>();

            OnSearchButtonPressed = new Command(
                CanOnSearchButtonPressed,
                OnSearchButtonPressedExecute
                );
            OnBuildGraphicsPressed = new Command(
                CanBuildGraphicsExecute,
                 OnBuildGraphicsExecute
                );


        }

        #endregion

        #region Methods

        #region On search button pressed

        private bool CanOnSearchButtonPressed(object p)
        {
            return true;
        }

        private void OnSearchButtonPressedExecute(object p)
        {
            SearchMethodAsync();
        }
        #endregion
        /// <summary>
        /// Asybchronously create the search results
        /// </summary>
        private async void SearchMethodAsync()
        {
            m_searchresult.Clear();
            
            try
            {
                if ((Start == new DateTime()) || (End == new DateTime()))
                    throw new Exception("Поля ввода дат имели неверный формат!");

                await Task.Run(() =>
                {
                    foreach (var note in archive.ArchiveProp)
                    {
                        if ((note.DateEnd >= Start) && (note.DateEnd <= End))
                        {
                            current.Dispatcher.Invoke(() =>
                            {
                                m_searchresult.Add(note);
                            });
                        }
                    }

                    if (SearchResult.Count <=0)
                    {
                        MessageBox.Show(current, "Не найдено никаких записей в этом интервале дат!",
                            "Project Manager", MessageBoxButton.OK, MessageBoxImage.Information);
                    }                    
                });
            }
            catch (Exception e)
            {
                CommonFunctions.MessageBoxCreate(e.Message
                        , System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information);
            }                                 
        }
        
        #region On Build Diagrams Pressed
        private bool CanBuildGraphicsExecute(object p)
        {
            return true;
        }
        /// <summary>
        /// Calculates and creates the main collections of arguments and initializes the function in 
        /// DiagramCreator that visualizes the diagrams
        /// </summary>
        /// <param name="p"></param>
        private void OnBuildGraphicsExecute(object p)
        {
            try
            {
                MonthCollectionProp.Clear();

                RevenueCollectionProp.Clear();

                CountCollectionProp.Clear();

                MiddleRevenueCollection.Clear();

                var MonthCollectionTemp = new ObservableCollection<DateTime>();

                var AmountofDays = new List<int>();

                double revenue = 0;

                int count = 0;

                var date = new DateTime();

                for (int i = 0; i < SearchResult.Count; i++)
                {
                    AmountofDays.Add(SearchResult[i].AmountOfDays.Days);

                    if (SearchResult[i].IsTheEnd == false)
                    {
                        count++;

                        revenue += SearchResult[i].Cost;
                    }
                    else
                    {
                        count++;

                        revenue += SearchResult[i].Cost;

                        date = SearchResult[i].DateEnd;

                        MonthCollectionTemp.Add(date);

                        RevenueCollectionProp.Add(revenue);

                        CountCollectionProp.Add(count);

                        MiddleRevenueCollection.Add(Math.Round(revenue/count, 2));

                        count = 0;

                        revenue = 0;
                    }
                }

                MonthCollectionProp = MonthCollectionTemp; //Triger will be fired when the data in
                //MonthCollectionProp changes, and that will call the method to draw diagram
                MaxAmountOfDays = AmountofDays.Max();

                MinAmountOfDays = AmountofDays.Min();

                var maxindex = AmountofDays.IndexOf((int)MaxAmountOfDays);

                var minindex = AmountofDays.IndexOf((int)MinAmountOfDays);

                MaxAmountOfDaysDate = SearchResult[maxindex].DateEnd;

                MinAmountOfDaysDate = SearchResult[minindex].DateEnd;

                MaxProjectName = SearchResult[maxindex].ProjectName;

                MinProjectName = SearchResult[minindex].ProjectName;
            }
            catch (Exception)
            {
                CommonFunctions.MessageBoxCreate("Выборка не построена!!! " +
                    "Введите диапазон дат и постройте выборку!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        
        #endregion

        #endregion
    }
}
