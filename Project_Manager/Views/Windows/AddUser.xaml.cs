﻿using Project_Manager.ViewModels.Windows;
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
        public AddUser(DbConnectionStringBuilder conStrBuilder)
        {
            AddUserViewModel m_vm = new AddUserViewModel(conStrBuilder);

            InitializeComponent();
        }
    }
}
