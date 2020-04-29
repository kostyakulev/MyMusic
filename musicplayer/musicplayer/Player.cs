using Android.Media;
using System.Collections.ObjectModel;
using System.IO;

using Xamarin.Forms;

namespace musicplayer
{
    public class Player : ContentPage
    {
        public MediaPlayer player;
        public string nameFile;
        
        public ObservableCollection<string> MusicItems { get; set; }
        

        public Player()
        {
           
        }
        public void Mp3() {
            
            var PhonePath = Android.OS.Environment.ExternalStorageDirectory.ToString();
            string newFilePath = Path.Combine(Directory.GetFiles(PhonePath, nameFile + ".mp3", SearchOption.AllDirectories));


            if (player == null)
            {
                player = new MediaPlayer();
                player.SetDataSource(newFilePath);
                player.Prepare();
                player.Start();
                
            }
            else
            {
                player.Reset();
                player.SetDataSource(newFilePath);
                player.Prepare();
                player.Start();
                
            }
        }

        

        public void Stop()
        {
            try
            {
                
                if (player == null) { }
                else if (player.IsPlaying == true)
                {
                    
                    player.Stop();
                    player.Release();
                    player = null;

                }
            }
            catch { }
        }
        public void Pause() { player.Pause();  }
        public void Start() { player.Start(); }
        
    }
}