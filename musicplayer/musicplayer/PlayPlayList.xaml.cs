using Android.Media;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace musicplayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPlayList : ContentPage
    {
        Player mp = new Player();
        
        public ObservableCollection<string> MusicItems { get; set; }
        
        public string itemcoll;



        public PlayPlayList(string playlistname)
        {
            InitializeComponent();
             
            string filetext = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), playlistname + ".rtf");
            var text = File.ReadAllText(filetext).Split(',');
            MusicItems = new ObservableCollection<string>(text);
            PLayList.BindingContext = MusicItems;
            mp.MusicItems = MusicItems;

        }

        private void PLayList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                PlayButton.Text = "||";
                mp.nameFile = PLayList.SelectedItem.ToString();
                mp.Mp3();
                Slider();
                mp.player.Completion += Player_Completion;
            }
            catch { }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                int id = (PLayList.ItemsSource as ObservableCollection<string>).IndexOf(PLayList.SelectedItem as string);
                var itemcoll = MusicItems[id - 1];
                PLayList.SelectedItem = itemcoll;
                mp.nameFile = PLayList.SelectedItem.ToString();
                mp.Mp3();
                mp.player.Completion += Player_Completion;
                PlayButton.Text = "||";
            }
            catch { }
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                int id = (PLayList.ItemsSource as ObservableCollection<string>).IndexOf(PLayList.SelectedItem as string);
                var itemcoll = MusicItems[id + 1];
                PLayList.SelectedItem = itemcoll;
                mp.nameFile = PLayList.SelectedItem.ToString();
                mp.Mp3();
                mp.player.Completion += Player_Completion;
                PlayButton.Text = "||";
            }
            catch { }
        }
        private void PlayButton_Clicked(object sender, EventArgs e)
        {
           
            try
            {
                mp.nameFile = PLayList.SelectedItem.ToString();
                if (mp.player == null)
                {

                    PlayButton.Text = "||";
                    mp.Mp3();
                    Slider();
                    mp.player.Completion += Player_Completion;
                }
                else if (mp.player.IsPlaying == true)
                {
                    mp.Pause();
                    PlayButton.Text = "➤";
                }
                else if (mp.player.IsPlaying == false)
                {
                    mp.Start();
                    Slider();
                    PlayButton.Text = "||";
                }

            }
            catch { }
        }
        private void Player_Completion(object sender, EventArgs e)
        {
            try
            {

                int id = (MusicItems as ObservableCollection<string>).IndexOf(mp.nameFile as string);
                itemcoll = MusicItems[id + 1];
                var last = MusicItems.Last();
                PLayList.SelectedItem = itemcoll;
                mp.nameFile = PLayList.SelectedItem.ToString();
                mp.Mp3();
                int x = 0;
                int y = last.CompareTo(mp.nameFile);
                if (y == x)
                {
                    mp.player.Completion += Player_Completion1;
                }
            }
            catch { }
        }

        private void Player_Completion1(object sender, EventArgs e)
        {
            PlayButton.Text = "➤";
            mp.Stop();
        }

        private void Slider()
        {
            sliderBar.Maximum = mp.player.Duration;
            mp.player.Completion += Player_Completion;


            Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
        }
        private bool UpdatePosition()
        {

            sliderTime.Text = $"Postion: {((int)mp.player.CurrentPosition)} / {(int)mp.player.Duration}";

            sliderBar.ValueChanged -= SliderBar_ValueChanged;
            sliderBar.Value = mp.player.CurrentPosition;
            sliderBar.ValueChanged += SliderBar_ValueChanged;

            return mp.player.IsPlaying;



        }

        private void SliderBar_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sliderBar.Value != mp.player.Duration)
            {
                mp.player.SeekTo((int)sliderBar.Value);
            }
        }
    }
}

