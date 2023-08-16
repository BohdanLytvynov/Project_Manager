
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace SerializationLibrary
{
    public static class JsonSerialization
    {
        /// <summary>
        /// Generic method can serialize objects of <T> where T is Repository<Client>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="SerializableObject"></param>
        public static void Serialize<T>(string path, T SerializableObject)            
        {
            string json = JsonConvert.SerializeObject(SerializableObject,
                Formatting.Indented);

            File.WriteAllText(path, json, Encoding.UTF8);
        }
               
        /// <summary>
        /// Deserializes Object of anonymous type
        /// </summary>
        /// <typeparam name="T"></typeparam> object that will be returned
        /// <typeparam name="Tin"></typeparam> Ckient Converter
        /// <typeparam name="Tin2"></typeparam> LogConverter
        /// <typeparam name="Tin3"></typeparam> LogRepositoryConverter
        /// <param name="path"></param>
        /// <param name="ObjectForDeserialization"></param>
        /// <param name="ClientConverter"></param>
        /// <param name="LogConverter"></param>
        /// <param name="RepConverter"></param>
        /// <returns></returns>
        public static T Deserealize<T, Tin>(string path, T ObjectForDeserialization,
            Tin [] converters)
            
            where Tin : JsonConverter
           
        {
            string json = File.ReadAllText(path, Encoding.UTF8);
            
            JsonSerializerSettings settings = new JsonSerializerSettings()
            { Converters = converters };

            return JsonConvert.DeserializeAnonymousType(json, ObjectForDeserialization,
                settings);            
        }
        /// <summary>
        /// Generic Method to Deserialize object of anonymous type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="DeserializableObject"></param>
        /// <returns></returns>
        public static T Deserealize<T>(string path, T DeserializableObject)
        {
            string json = File.ReadAllText(path, Encoding.UTF8);

            return JsonConvert.DeserializeAnonymousType<T>(json, DeserializableObject);
        }
        
    }
}
