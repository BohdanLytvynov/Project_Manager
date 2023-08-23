using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project_Manager
{
    public class Settings
    {
        #region Fields
        private static bool firststart = true; //App starts first?

        private static string pathtodatabase = String.Empty;//Path to db

        private static string pathtoarchive;//path to archive
        #endregion

        public bool FirstStart
        {
            get => firststart;

            set => firststart = value;
        }
               
        public string PathToDataBase
        {
            get => pathtodatabase;

            set => pathtodatabase = value;
        }

        public string PathToArchive
        {
            get => pathtoarchive;

            set => pathtoarchive = value;
        }
    }
}
