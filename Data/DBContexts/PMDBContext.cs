using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBContexts
{
    public class PMDBContext : DbContext
    {
        #region Fields

        #endregion

        #region Properties

        #region DBSets

        public DbSet<Salt> Salts { get; set; }

        public DbSet<ProjectDB> ProjectsTable { get; set; }

        public DbSet<User> UsersTable { get; set; }

        public DbSet<ArchiveNote> ArchiveNotes { get; set; }

        #endregion

        #endregion

        #region Ctor
        public PMDBContext(string connectionString):base(connectionString)
        {
            Database.Delete();

            Database.Create();
        }
        #endregion

        #region Methods
       
        #endregion
    }
}
