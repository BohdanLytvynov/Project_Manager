using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Encryption
{
    public class DBEncryption
    {
        #region Ctor

        private readonly Encoding m_Encoding;

        #endregion

        #region Properties

        public Encoding Encoding { get=> m_Encoding; }

        #endregion

        #region Ctor
        public DBEncryption(Encoding enc)
        {
            m_Encoding = enc;
        }
        #endregion

        public string GenerateRandomString(int count)            
        { 
            Random random = new Random();
            
            byte[] bytes = new byte[count];

            for (int i = 0; i <= count; i++)
            {
                bytes[i] = (byte)random.Next(0, 256);
            }

            return m_Encoding.GetString(bytes);
        }

        public static char[] StringToChars(string str)
        {
            var chars = new char[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                chars[i] = str[i];
            }

            return chars;
        }

        
        public string Hash(byte[] array, byte [] saltBytes)
        { 
            byte[] combinedBytes = new byte[array.Length + saltBytes.Length];
            
            int last = 0;

            for (int i = 0; i < array.Length; i++)
            {
                combinedBytes[i] = array[i];

                last = i;
            }

            for (int i = ++last; i < combinedBytes.Length; i++)
            {
                combinedBytes[i] = saltBytes[i];
            }

            return m_Encoding.GetString
                (SHA256.Create().ComputeHash(combinedBytes));            
        }
    }
}
