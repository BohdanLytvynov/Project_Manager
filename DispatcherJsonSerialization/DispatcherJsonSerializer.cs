using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace DispatcherJsonSerialization
{
    public static class DispatcherJsonSerializer
    {
        private static Window m_dispatcherobject; //Window instance

        public static Window DispatcherObject
        {
            get => m_dispatcherobject;

            set => m_dispatcherobject = value;
        }
        /// <summary>
        /// Serialize object using the dispatcher of the instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="ObjectForSerialization"></param>
        public static void SerializeWithDispatcher<T>(string path, T ObjectForSerialization)
        {
            string json = String.Empty;

            m_dispatcherobject.Dispatcher.Invoke(() =>
            {
                json = JsonConvert.SerializeObject(ObjectForSerialization,
                    Formatting.Indented);
            });

            File.WriteAllText(path, json, Encoding.UTF8);
        }

    }
}
