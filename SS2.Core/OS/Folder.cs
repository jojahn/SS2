using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.OS
{
    public class Folder
    {
        public string Location { get; set; }

        public Folder(string folderName = "avalon_ss2_E6C3D074-D0BB-48A7-AC02-A54E7F2AFCDA")
        {
            TestAndCreateFolder(folderName);
        }

        private void TestAndCreateFolder(string folderName)
        {
            string parentFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            if (!new DirectoryInfo(parentFolder).Exists)
            {
                parentFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            }
            Location = Path.Join(parentFolder, folderName);
            if (!new DirectoryInfo(Location).Exists)
            {
                Directory.CreateDirectory(Location);
            }
        }

        public void SaveObject<T>(string fileName, T obj)
        {
            string filePath = Path.Join(Location, fileName);
            using (FileStream stream = File.OpenWrite(filePath))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                serializer.Serialize(stream, obj);
            }
        }

        public T LoadObject<T>(string fileName)
        {
            string filePath = Path.Join(Location, fileName);
            using (FileStream stream = File.OpenRead(filePath))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
