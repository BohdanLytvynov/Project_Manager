using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RepositoryFunctionsLibrary
{
    public static class RepositoryFunctions
    {
        public static string GetPathToDBOnCurrentMachine(string pathtodb)//Path to folder with file "@"
        {
            if (pathtodb == null)
                return String.Empty;

            string path = Environment.CurrentDirectory;

            var array = path.Split('\\');

            int length = array.Length - 2;

            var result = FormString(array, length);

            return result + pathtodb;
        }

        /// <summary>
        /// Forms a string from array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string FormString(string[] array, int length)
        {
            string result = String.Empty;

            for (int i = 0; i < length; i++)
            {
                result += array[i] + @"\";
            }

            return result;
        }
        /// <summary>
        /// Checks if file with db exists
        /// </summary>
        public static void CheckIfFIleExists(string pathtodb)
        {
            FileInfo file = new FileInfo(pathtodb);

            if (!file.Exists)
            {
                FileStream fs = File.Create(pathtodb);

                fs.Close();

                fs.Dispose();
            }
        }



    }
}
