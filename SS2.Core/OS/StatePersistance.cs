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
        private static readonly string _gameFileName = "avalon_ss2_E6C3D074-D0BB-48A7-AC02-A54E7F2AFCDA_current_game.xml";

        private Folder _folder = new Folder();

        public void SaveGame(SavedGame game)
        {
            _folder.SaveObject<SavedGame>(_gameFileName, game);
        }

        public SavedGame LoadGame()
        {
            return _folder.LoadObject<SavedGame>(_gameFileName);
        }
    }
}
