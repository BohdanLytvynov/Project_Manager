using Data;
using Data.DBContexts;
using Data.Models;
using Encryption;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBEncryption enc = new DBEncryption(new UTF8Encoding());

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

            string pass1 = "1234";

            string kw2 = "C++";

            Salt s1 = new Salt(Guid.NewGuid(), enc.GenerateRandomString(pass1.Length),
                enc.GenerateRandomString(kw2.Length));

            User u1 = new User(Guid.NewGuid(), "admin", 
                
                , "C++");

            

            db.UsersTable.Add(u1);
            
            db.SaveChanges();
            
            int e = db.SaveChanges();

            var r = (from u in db.UsersTable
                     where u.Login == "admin"
                     select u).Include(u => u.Projects).ToList();

            Console.WriteLine("Finish!");
        }
    }
}
