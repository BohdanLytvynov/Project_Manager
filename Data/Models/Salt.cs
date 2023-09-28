using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Salts")]
    public class Salt
    {
        #region Prim Key
        [Key]
        [ForeignKey("User")]
        public Guid Id { get; set; }

        #endregion

        #region Info

        public string PassSalt { get; set; }

        public string KeyWordSalt { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
               
        #endregion

        #region Ctor

        public Salt()
        {

        }

        public Salt(Guid id, string PassSalt, string KeyWordSalt)
        {
            Id = id;

            this.PassSalt = PassSalt;

            this.KeyWordSalt = KeyWordSalt;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Id} {PassSalt} {KeyWordSalt}";
        }

        #endregion
    }
}
