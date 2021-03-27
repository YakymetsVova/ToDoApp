using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApplication;

namespace TodoApplication.Services
{
    class FileIOService<TItem>
    {
        private readonly string PATH;
        public FileIOService(string path)
        {
            PATH = path;
        }

        public List<TItem> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new List<TItem>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TItem>>(fileText);
            }
        }

        public void SaveData(List<TItem> tasks)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(tasks);
                writer.Write(output);
            }
        }
    }
}
