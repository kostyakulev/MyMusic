using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace musicplayer
{

        public partial class MainPage : MasterDetailPage
    {
        Allmusic allmusic = new Allmusic();
        Radio radio = new Radio();
        Playlist playlist = new Playlist();
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage(allmusic)
            {
            BarBackgroundColor = Color.FromHex("#000000")
            };
                                       
            IsPresented = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(playlist)
            {
                BarBackgroundColor = Color.FromHex("#000000")
            };
           
            IsPresented = false;
        }
        private  void Button2_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(allmusic)
            {
                BarBackgroundColor = Color.FromHex("#000000")
            };

            IsPresented = false;
        }
       

        private void Button_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(radio)
            {
                BarBackgroundColor = Color.FromHex("#000000")
            };
            IsPresented = false;
        }

       
    }
}
