using Android.Widget;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace musicplayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Playlist : ContentPage
    {
        public ObservableCollection<string> ListItems { get; set; }
        public string PlayListName;
        
        public Playlist()
        {
            InitializeComponent();
            Listload();
            

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new ListAdd());
            
        }
        public void Listload(){
            var documents = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            var newfilePath = Directory.EnumerateFiles(documents, "*.rtf", SearchOption.AllDirectories).Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
            ListItems = new ObservableCollection<string>(newfilePath);
            PLayLists.BindingContext = ListItems;

    
        }

        public async void PLayLists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
            PlayListName = PLayLists.SelectedItem.ToString();
            Console.WriteLine(PlayListName);
            
            await Navigation.PushAsync(new PlayPlayList(PlayListName));

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Listload();
        }
    }

}