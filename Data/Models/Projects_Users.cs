using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Pr_User_Map")]
    public class Projects_Users
    {
        #region Prim Key

        [Key]        
        public int Id { get; set; }

        #endregion

        #region NavProperties

        #endregion
    }
}
