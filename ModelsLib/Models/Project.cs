using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ViewModelBaseLib.ViewModelBase;
using System.Windows.Controls;

namespace ModelsLib.Models
{
    public class ProjectUrgency : IEnumerable<string>
    {
        private readonly string[] array;

        public string this[int index]
        {
            get => this.array[index];

            set => this.array[index] = value;
        }

        public ProjectUrgency()
        {
            array = new string [] { "Срочный","Обычный", "Приоритет"};
        }

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return array.GetEnumerator();
        }
    }

    public class Project : ViewModelBase
    {
        #region Project Fields

        private Brush m_borderbrush; //The project border's color will be set according to the urgency

        private string name;//Project's name

        private string description; //Project's description

        private DateTime date; //Date of start

        private bool m_isCostcorrect; //Is Cost correct

        private string str_Cost; //String Cost

        private double cost; //Project cost

        private string urgency; //project urgency

        private string customername; //Customer name                       
        [NonSerialized]
        private int m_SelectedcbvalueIndex = 0;

        #endregion

        #region Project Properties

        public int SelectedCBIndex
        {
            get => m_SelectedcbvalueIndex;

            set
            {
                m_SelectedcbvalueIndex = value;

                OnPropertyChanged(nameof(SelectedCBIndex));
            }
        }
                             
        public bool IsCostCorrect
        {
            get => m_isCostcorrect;

            set => SetProperty<bool>(ref m_isCostcorrect, value, nameof(IsCostCorrect));
        }

        public string Str_Cost
        {
            get => str_Cost;

            set 
            {
                SetProperty<string>(ref str_Cost, value, nameof(Str_Cost));

                double result;

                if (double.TryParse(str_Cost, out result))
                {
                    IsCostCorrect = true;
                    Cost = result;                   
                }
                else
                {
                    IsCostCorrect = false;
                }
            }
        }

        public string Urgency
        {
            get => urgency;

            set
            {
                SetProperty<string>(ref urgency, value, nameof(Urgency));

                SetBorderColorAccordingToUrgency();
            }
        }

        public string Name
        {
            get => name;

            set => SetProperty<string>(ref name, value, nameof(Name));
        }
        public string Description
        {
            get => description;

            set => SetProperty<string>(ref description, value, nameof(Description));
        }
        public DateTime Date
        {
            get => date;

            set => SetProperty<DateTime>(ref date, value, nameof(Date));
        }

        public double Cost
        {
            get => cost;

            set => SetProperty<double>(ref cost, value, nameof(Cost));
        }

        public Brush BorderBrushColor
        {
            get => m_borderbrush;

            set => SetProperty<Brush>(ref m_borderbrush, value, nameof(BorderBrushColor));
        }

        public string CustomerName
        {
            get => customername;

            set => SetProperty<string>(ref customername, value, nameof(CustomerName));
        }

        public Project(string name, string description, DateTime date, string cost
            , string urgency, string CustomerName)
        {
            this.urgency = urgency;

            this.name = name;

            this.description = description;

            this.date = date;

            this.str_Cost = cost;

            this.customername = CustomerName;            
        }
        /// <summary>
        /// Sets the project border's color according to the urgency of the project
        /// </summary>
        private void SetBorderColorAccordingToUrgency()
        {
            switch (Urgency)
            {
                case "Срочный":
                    BorderBrushColor = Brushes.Red;
                    break;
                case "Обычный":
                    BorderBrushColor = Brushes.Green;
                    break;
                case "Приоритет":
                    BorderBrushColor = Brushes.Black;
                    break;
                default:
                    break;
            }
        }

        #region Operators of equalation

       
        #endregion

        #endregion
    }
}
