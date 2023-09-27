using Controllers.CRUDController;
using Controllers.Managers;
using Project_Manager.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_Manager.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser(DbConnectionStringBuilder connectionStringBuilder)
        {
            AddUserViewModel m_vm = new AddUserViewModel(connectionStringBuilder);

            InitializeComponent();

            this.DataContext = m_vm;
        }
    }
}
