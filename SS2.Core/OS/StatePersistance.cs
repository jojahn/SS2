using SS2.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SS2.Core.OS
{
    public class StatePersistance
    {
        private static readonly string _folderName = "avalon_ss2_E6C3D074-D0BB-48A7-AC02-A54E7F2AFCDA";
        private static readonly string _gameFileName = "avalon_ss2_E6C3D074-D0BB-48A7-AC02-A54E7F2AFCDA_current_game.xml";

        class Files
        {
            private string _path;

            public Files(string folderName)
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
                _path = Path.Join(parentFolder, folderName);
                Directory.CreateDirectory(_path);
            }

            public void Save<T>(string fileName, T obj)
            {
                string filePath = Path.Join(_path, fileName);
                using (FileStream stream = File.OpenWrite(filePath))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
                    serializer.Serialize(stream, obj);
                }
            }

            public T Load<T>(string fileName)
            {
                string filePath = Path.Join(_path, fileName);
                using (FileStream stream = File.OpenRead(filePath))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stream);
                }
            }
        }

        private Files _files = new Files(_folderName);

        public void SaveGame(SavedGame game)
        {
            _files.Save<SavedGame>(_gameFileName, game);
        }

        public SavedGame LoadGame()
        {
            return _files.Load<SavedGame>(_gameFileName);
        }
    }
}
