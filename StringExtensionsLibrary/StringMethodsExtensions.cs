using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtensionsLibrary
{
    public static class StringMethodsExtensions
    {
        /// <summary>
        /// Compare two strings. First converts them to int arrays, 
        /// according to ASCII, if letters in the strings are written in Latin,
        /// but if letters are Cyrillic, they will be converted to utf-8 numeric codes
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CompareStrings(this string first, string second)
        {
            if (first.Length != second.Length)
                return false;

            var firstIntArray = StringToIntArrayConverter(first);

            var SecondIntArray = StringToIntArrayConverter(second);

            return CompareIntArrays(firstIntArray, SecondIntArray);
        }
        /// <summary>
        /// Converts String to int array
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        private static int[] StringToIntArrayConverter(string String)
        {
            var result = new int[String.Length];

            for (int i = 0; i < String.Length; i++)
            {
                result[i] = (String[i]);
            }
            return result;
        }
        /// <summary>
        /// Compares two int arrays
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        private static bool CompareIntArrays(int[] array1, int [] array2)
        {
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }
            return true;
        }
    }
}
