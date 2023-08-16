using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ModelsLib.Models;
using SerializationLibrary;
using RepositoryFunctionsLibrary;
using System.IO;

namespace Storage
{
    public class CollectionsDB
    {        
        
        private ObservableCollection<Project>
         newprogects, //new projects collection

         projectsinwork, //projects in work collection

         projectswaiting, //projects waiting collection

         projectsestablished, //projects established collections

         projectspayed; //projects paid collection

        public ObservableCollection<Project> NewProjects
        {
            get => newprogects;

            set => newprogects = value;
        }

        public ObservableCollection<Project> ProjectsInWork
        {
            get => projectsinwork;

            set => projectsinwork = value;
        }

        public ObservableCollection<Project> ProjectsWaiting
        {
            get => projectswaiting;

            set => projectswaiting = value;
        }

        public ObservableCollection<Project> ProjectsEstablished
        {
            get => projectsestablished;

            set => projectsestablished = value;
        }

        public ObservableCollection<Project> ProjectsPayed
        {
            get => projectspayed;

            set => projectspayed = value;
        }
        
        public CollectionsDB()
        {
            newprogects = new ObservableCollection<Project>();

            projectsinwork = new ObservableCollection<Project>();

            projectswaiting = new ObservableCollection<Project>();

            projectsestablished = new ObservableCollection<Project>();

            projectspayed = new ObservableCollection<Project>();
        }
        
    }
}
