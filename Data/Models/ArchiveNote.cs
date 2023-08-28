using Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("ArchiveNotes")]
    public class ArchiveNote
    {
        #region Properties

        #region PrimaryKey

        public Guid Id { get; set; }

        #endregion

        #region Navigation Properties

        public Guid UserId { get; set; }

        public User User { get; set; }

        #endregion

        public string ProjectName
        {
            get; set;
        }

        public double Cost
        {
            get; set;
        }

        public TimeSpan AmountOfDays
        {
            get; set;
        }

        public DateTime DateEnd
        {
            get; set;
        }

        public Currency ProjectCurrency
        { 
            get; set; 
        }

        public bool IsTheEnd
        {
            get; set;
        }

        #endregion

        #region Ctor

        public ArchiveNote()
        {

        }

        public ArchiveNote(Guid id, string projectName, double cost, TimeSpan amountOfDays,
            DateTime dateEnd, bool isTheEnd, Currency currrency)
        {
            Id = id;

            ProjectName = projectName;

            Cost = cost;

            AmountOfDays = amountOfDays;

            DateEnd = dateEnd;

            IsTheEnd = isTheEnd;

            ProjectCurrency = currrency;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{ProjectName} | {Cost} | {ProjectCurrency} | {AmountOfDays} | {DateEnd} | {IsTheEnd}";
        }
        #endregion
    }
}
