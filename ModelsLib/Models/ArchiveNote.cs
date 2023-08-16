using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.ViewModelBase;

namespace ModelsLib.Models
{
    public class ArchiveNote : ViewModelBase
    {
        #region Fields

        private string m_projectname; //Name of a project

        private double m_cost;//Project cost

        private TimeSpan m_amountofdays; //amount of days that were spent on work with the project

        private DateTime m_dateEnd; //The date of project finishing

        private bool m_istheend; //If the current day is the end of month

        #endregion

        #region Properties

        public string ProjectName
        {
            get => m_projectname;

            set => SetProperty<string>(ref m_projectname, value, nameof(ProjectName));
        }

        public double Cost
        {
            get => m_cost;

            set => SetProperty<double>(ref m_cost, value, nameof(Cost));
        }

        public TimeSpan AmountOfDays
        {
            get => m_amountofdays;

            set => SetProperty<TimeSpan>(ref m_amountofdays, value, nameof(AmountOfDays));
        }

        public DateTime DateEnd
        {
            get => m_dateEnd;

            set => SetProperty<DateTime>(ref m_dateEnd, value, nameof(DateEnd));
        }

        public bool IsTheEnd
        {
            get => m_istheend;

            set => m_istheend = value;
        }

        #endregion

        #region Constructor

        public ArchiveNote(string name, double cost, TimeSpan daysamount, DateTime enddate,
            bool istheendofmonth)
        {
            ProjectName = name;

            Cost = cost;

            AmountOfDays = daysamount;

            DateEnd = enddate;

            IsTheEnd = istheendofmonth;
        }

        #endregion
    }
}
