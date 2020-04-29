using Android.Media;
using System;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;
using System.IO;

namespace musicplayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Radio : ContentPage
    {
        public ObservableCollection<string> RadioItem { get; set; }
        private MediaPlayer player;
        string radioitem;

        public Radio()
        {
            InitializeComponent();
            LoadRadiolist();
            ButtonCheckes();
        }

        
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
           

            await Navigation.PushAsync(new AddRadio());
           
        }

        private void RadioList_ItemSelected( object sender,SelectedItemChangedEventArgs args)
        {
            play.Text = "➤";
            if (player == null) { }
            else if (player.IsPlaying == true) {
               player.Stop();
                
               player.Release();
               player = null;
            }
           
        }

        private void  Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                radioitem = radioList.SelectedItem.ToString();
                string filetext = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), radioitem + ".txt");
                if (File.Exists(filetext))
                {
                    File.Delete(filetext);
                }
                LoadRadiolist();
            }
            catch { }
        }
       
        public void Mp3()
        {
           
            var radioitem = radioList.SelectedItem.ToString();
            string filetext = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), radioitem + ".txt");
            string text = File.ReadAllText(filetext);

            if (player == null)
            {
                player = new MediaPlayer();
                player.SetDataSource(text);
                player.Prepare();
                player.Start();
            }
            else {
                player.Reset();
                player.SetDataSource(text);
                player.Prepare();
                player.Start();
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            ButtonCheckes();

                      
        }
        private void ButtonCheckes() {
            try


            {
                if (player == null )
                {
                    Mp3();
                    play.Text = "||";
                }
                else if (player.IsPlaying == true)
                {

                    player.Pause();
                    play.Text = "➤";
                }
                else if (player.IsPlaying == false)
                {
                    player.Start();
                    play.Text = "||";
                }
            }
            catch { }
        }
        private void Stop_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (player.IsPlaying == true)
                {
                    player.Stop();
                    player.Release();
                    
                }
            }
            catch { }
           
        }

       
        public void LoadRadiolist() {
            var documents = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            var newfilePath = Directory.EnumerateFiles(documents, "*.txt", SearchOption.AllDirectories).Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
            RadioItem = new ObservableCollection<string>(newfilePath);

            radioList.BindingContext = RadioItem;
        }

        
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            LoadRadiolist();
        }
    }
    
}