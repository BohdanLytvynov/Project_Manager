using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ModelsLib.Models;

namespace Storage
{
    public class Archive
    {
        private ObservableCollection<ArchiveNote> archive; //Collection of archive notes

        private double m_commonSum; //r revenue 

        public ObservableCollection<ArchiveNote> ArchiveProp
        {
            get => archive;
        }

        public double CommonSum
        {
            get => m_commonSum;

            set => m_commonSum = value;
        }

        public Archive()
        {
            archive = new ObservableCollection<ArchiveNote>();
        }
    }
}
