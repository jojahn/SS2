using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS2.Core.OS
{
    public class Audio
    {
        public Audio(string file)
        {
            ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(GetStreamFromFile(file));
            player.Play();
        }

        Stream GetStreamFromFile(string filename)
        {
            throw new NotImplementedException();
            /* var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("YourApp." + filename);
            return stream; */
        }
    }
}
