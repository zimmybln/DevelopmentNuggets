using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZimmysDevelopmentNuggets.Components
{
    public class FileCollection
    {
        public string Name { get; set; }

        public List<string> Files { get; set; }
    }


    public class FileCollectionsContainer
    {
        public static IEnumerable<FileCollection> Load(string rootDirectory)
        {
            // Dateinamen erzeugen
            string fileName = BuildFileName(rootDirectory);

            if (!File.Exists(fileName))
                return null;

            // Deserialisieren
            try
            {
                var json = File.ReadAllText(fileName);

                if (!string.IsNullOrEmpty(json))
                {
                    return JsonConvert.DeserializeObject<List<FileCollection>>(json);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }

        public static void Save(List<FileCollection> items, string rootDirectory)
        {
            // Dateinamen erzeugen
            string fileName = BuildFileName(rootDirectory);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
                
            // serialisieren
            try
            {
                var json = JsonConvert.SerializeObject(items);

                File.WriteAllText(fileName, json);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>
        /// Builds the name of the file depending on the environment
        /// </summary>
        /// <returns></returns>
        private static string BuildFileName(string rootDirectory)
        {
            return rootDirectory + $"\\{Environment.UserName}.json";
        }
    }
}
