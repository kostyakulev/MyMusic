
using Android.Media;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Android.Provider.MediaStore;

namespace musicplayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Allmusic : ContentPage
    {

        public ObservableCollection<string> MusicItems { get; set; }

        Player mp = new Player();
        
        public string itemcoll;

        public Allmusic()
        {
            InitializeComponent();
            MusicLoadList();
            

        }

        public void MusicLoadList()
        {

            var PhonePath = Android.OS.Environment.ExternalStorageDirectory.ToString();
            var filePath = Directory.GetFiles(PhonePath, "*.mp3", SearchOption.AllDirectories).Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
            MusicItems = new ObservableCollection<string>(filePath);
            mp.MusicItems = MusicItems;
            
            listmusic.BindingContext = MusicItems;

        }

        private void Listmusic_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try {
                
                    PlayButton.Text = "||";
                    mp.nameFile = listmusic.SelectedItem.ToString();
                    mp.Mp3();
                    Slider();
                    mp.player.Completion += Player_Completion;
            }
            catch { }
            }
        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Up();
        }
        private void Button_Clicked_2(object sender, System.EventArgs e)
        {
            Down();
        }
        private void Up() {
            try
            {
                int id = (listmusic.ItemsSource as ObservableCollection<string>).IndexOf(listmusic.SelectedItem as string);
                var itemcoll = MusicItems[id - 1];
                listmusic.SelectedItem = itemcoll;
                mp.nameFile = listmusic.SelectedItem.ToString();
                mp.Mp3();
                mp.player.Completion += Player_Completion;
                PlayButton.Text = "||";
            }
            catch { }
        }
        private void Down()
        {
            try
            {
                int id = (listmusic.ItemsSource as ObservableCollection<string>).IndexOf(listmusic.SelectedItem as string);
                var itemcoll = MusicItems[id + 1];
                listmusic.SelectedItem = itemcoll;
                mp.nameFile = listmusic.SelectedItem.ToString();
                mp.Mp3();
                mp.player.Completion += Player_Completion;
                PlayButton.Text = "||";
            }
            catch { }
        }

        private void Button_Clicked_1(object sender, System.EventArgs e)
        {
            
            try
            {
                
                if (mp.player == null)
                {
                    
                    PlayButton.Text = "||";

                    mp.Mp3();
                    Slider();
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
        private void Slider()
        {
            try
            {
                sliderBar.Maximum = mp.player.Duration;
                
                Device.StartTimer(TimeSpan.FromSeconds(0.5), UpdatePosition);
            }
            catch { }
        }
        private bool UpdatePosition()
        {

            sliderTime.Text = $"Postion: {((int)mp.player.CurrentPosition)} / {(int)mp.player.Duration}";

            sliderBar.ValueChanged -= SliderBar_ValueChanged;
            sliderBar.Value = mp.player.CurrentPosition;
            sliderBar.ValueChanged += SliderBar_ValueChanged;

            return mp.player.IsPlaying;



        }

        private void Player_Completion(object sender, EventArgs e)
        {
            try
            {
                var last = MusicItems.Last();
                int x = 0;
                int y = last.CompareTo(mp.nameFile);
                if (y == x)
                {
                    mp.player.Completion += Player_Completion1;
                }
                else if (y != x)
                {
                    Down();
                }
            }
            catch { }
        }

        private void Player_Completion1(object sender, EventArgs e)
        {
            PlayButton.Text = "➤";
            mp.Stop();
        }




        private void SliderBar_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                if (sliderBar.Value != mp.player.Duration)
                {
                    mp.player.SeekTo((int)sliderBar.Value);
                }
            }
            catch { }
        }

       
    }
}

       
    
