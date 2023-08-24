using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Connection!");

            //Calculate path to DB

            string m_pathToExe = Environment.CurrentDirectory;

            string pathToDB = m_pathToExe + @"\" + "DataBase" + @"\" + "PMDB.mdf";

            //ConnectToLocalDB

            DbConnectionStringBuilder csbuild =
                new DbConnectionStringBuilder();

            csbuild.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                $"AttachDbFilename={pathToDB};" +
                "Integrated Security=True;Connect Timeout=30";

            PMDBContext db = new PMDBContext(csbuild.ConnectionString);

            //User u1 = new User(Guid.NewGuid(), "admin", "123", "C++");

            //User u2 = new User(Guid.NewGuid(), "User", "123", "C#");

            //db.UsersTable.Add(u1);

            //db.UsersTable.Add(u2);

            //db.SaveChanges();

            //db.ProjectsTable.Add(new ProjectDB(Guid.NewGuid(), "admProj",
            //    "", "", DateTime.Now.Date, 500, Urgency.Срочний)
            //{User = u1 });

            //db.ProjectsTable.Add(new ProjectDB(Guid.NewGuid(), "userProj",
            //    "", "", DateTime.Now.Date, 200, Urgency.Обичний)
            //{ User = u2 });

            //int e = db.SaveChanges();

            var r = (from u in db.UsersTable
                     where u.Login == "admin"
                     select u).Include(u => u.Projects).ToList();

            Console.WriteLine("Finish!");
        }
    }
}
