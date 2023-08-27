using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public enum Urgency : byte
    { 
        Срочний =0, Обичний, Приоритет
    }


    [Table("Projects")]
    public class ProjectDB
    {
        #region Properties

        #region Primary Key

        [Key]
        public Guid Id { get; set; }

        #endregion

        #region Project Info

        public string Name { get; set; }

        public string Description { get; set; }

        public string CustomerName { get; set; }

        public DateTime DateOfStart { get; set; }

        public double Cost { get; set; }

        public Urgency ProjectUrgency { get; set; }

        #endregion

        #region Navigation Properties
        
        public User User { get; set; }

        public Guid UserId { get; set; }

        #endregion

        #endregion

        #region Ctor
        public ProjectDB()
        {

        }

        public ProjectDB(Guid id, string name, string description,
            string customerName, DateTime dateOfSatrt, double cost,
            Urgency urgency)
        {
            Id = id;

            Name = name;

            Description = description;

            CustomerName = customerName;

            DateOfStart = dateOfSatrt;

            Cost = cost;

            ProjectUrgency = urgency;
        }

        public ProjectDB(Guid id) : this(id, "Имя проекта", "Описание",
            "Имя Заказчика", DateTime.Now.Date, 0, Urgency.Приоритет)
        {

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Name} | {Description} | {CustomerName} | {DateOfStart.Date} |" +
                $" {Cost} | {ProjectUrgency}";
        }

        #endregion
    }
}
