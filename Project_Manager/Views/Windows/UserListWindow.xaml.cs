using Project_Manager.ViewModels.Windows;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserListWindow.xaml
    /// </summary>
    public partial class UserListWindow : Window
    {
        UserListWindowViewModel m_vm;

        public UserListWindow()
        {
            InitializeComponent();

            m_vm = new UserListWindowViewModel();

            this.DataContext = m_vm;
        }
    }
}
