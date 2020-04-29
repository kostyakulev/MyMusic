using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace musicplayer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRadio : ContentPage
    {
        Radio radio = new Radio();
        public AddRadio()
        {
            InitializeComponent();
           

        }




        private async void Button_Clicked(object sender, EventArgs e)
        {
            var name = nameRadio.Text;
            var url = urlRadio.Text;
            var documents = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));
            var filename = System.IO.Path.Combine(documents, name+".txt");
            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, url);
            }
            else {
                File.WriteAllText(filename, url);
            }

            
            await Navigation.PopAsync();
            

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            
            await Navigation.PopAsync();
            
        }
    }
}