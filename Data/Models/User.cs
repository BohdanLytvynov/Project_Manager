using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Users")]
    public class User
    {
        #region Properties

        #region Prim Key

        public Guid Id { get; set; }

        #endregion

        #region Info

        public string Login { get; set; }

        ///Sensative data

        public string Password { get; set; }

        public string KeyWord { get; set; }

        #endregion

        #endregion

        #region Navigation Properties

        public ICollection<ProjectDB> Projects { get; set; }

        #endregion

        #region Ctor
        public User()
        {
            Projects = new List<ProjectDB>();
        }

        public User(Guid id, string login, string password, string keyword)
        {
            Id = id;

            Login = login;

            Password = password;

            KeyWord = keyword;

            Projects = new List<ProjectDB>();
        }
        #endregion

        #region Methods

        #endregion
    }
}
