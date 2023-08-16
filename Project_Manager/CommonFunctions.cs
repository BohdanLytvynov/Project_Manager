using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Manager
{
    public static class CommonFunctions
    {
        /// <summary>
        /// Creates a messagebox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="button"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static MessageBoxResult MessageBoxCreate(string text, MessageBoxButton button,
            MessageBoxImage image)
        {
            return MessageBox.Show(text, "Project Manager", button, image);
        }
    }
}
