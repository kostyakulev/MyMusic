using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace musicplayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListAdd : ContentPage
    {
        public ObservableCollection<string> PlayListItems { get; set; }
        public ObservableCollection<string> MusicItems { get; set; }
        Playlist playlist = new Playlist();

        string item;
        
        
        public ListAdd()
        {
            InitializeComponent();
            MusicLoad();
            
           
        }
        private void MusicLoad() {
            var PhonePath = Android.OS.Environment.ExternalStorageDirectory.ToString();
            var filePath = Directory.GetFiles(PhonePath, "*.mp3", SearchOption.AllDirectories).Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
            MusicItems = new ObservableCollection<string>(filePath);
            PlayListCreate.BindingContext = MusicItems;
        }
        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var listname = Listname.Text;
            string result = String.Join(",", PlayListItems.ToArray());
            var documents = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            var filename = System.IO.Path.Combine(documents, listname + ".rtf");
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, result);
            }
            else
            {
                File.WriteAllText(filename, result);
            }

            playlist.Listload();         
            await Navigation.PopAsync();

        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            var selectitem = checkBox.BindingContext.ToString();
            item = selectitem;
            if (PlayListItems == null)
            {
                PlayListItems = new ObservableCollection<string>();
            }
            if (checkBox.IsChecked == true) {
                PlayListItems.Add(item);                
                
            }
            else if(checkBox.IsChecked == false){
                PlayListItems.Remove(item);
            }
               
            
        }

         
       


    }
}
