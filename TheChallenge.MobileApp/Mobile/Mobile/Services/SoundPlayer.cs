using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.SimpleAudioPlayer;

namespace Mobile.Services
{
    public static class SoundPlayer
    {
        public static void PlaySound(string filename, bool loop = false)
        {
            var player = CrossSimpleAudioPlayer.Current;
            player.Load(GetSoundStream(filename));
            player.Loop = loop;
            player.Play();
            if (loop)
            {
                Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith(_ => player.Stop());
            }
        }

        private static Stream GetSoundStream(string filename)
        {
            return typeof(SoundPlayer).Assembly.GetManifestResourceStream("Mobile.Sounds." + filename);
        }
    }
}