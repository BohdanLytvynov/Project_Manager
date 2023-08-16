using ModelsLib.Models;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBaseLib.ViewModelBase;


namespace Project_Manager.ViewModels
{
    public class ViewModelCommon : ViewModelBase
    {
        #region Fields
        
        #region Settings
        protected static Settings settings; //Settings class (holds settings)
        #endregion

        #region Archive
        protected static Archive archive; //collection of archive notes
        #endregion

        #region ModelCollection
        protected static CollectionsDB collectionsdb; //holds collctions of projects
        #endregion

        protected ProjectUrgency pu; //project urgency
        #endregion

        #region Properties
        public string Path //Path to Db
        {
            get => settings.PathToDataBase;

            set
            {
                settings.PathToDataBase = value;

                OnPropertyChanged();
            }
        }

        public string PathToArchive //Path to archive
        {
            get => settings.PathToArchive;

            set
            {
                settings.PathToArchive = value;

                OnPropertyChanged();
            }
        }

        public CollectionsDB CollectionsDB 
        {
            get => collectionsdb;            
        }
        
        

        #endregion
    }
}
