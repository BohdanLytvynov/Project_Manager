using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PMDBContext : DbContext
    {
        #region Fields

        #endregion

        #region Properties

        #region DBSets

        public DbSet<ProjectDB> ProjectsTable { get; set; }

        public DbSet<User> UsersTable { get; set; }       

        #endregion

        #endregion

        #region Ctor
        public PMDBContext(string connectionString):base(connectionString)
        {
          
        }
        #endregion

        #region Methods
       
        #endregion
    }
}
