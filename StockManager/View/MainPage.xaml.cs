using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using StockManager;

namespace StockManager
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);//1st.Pageのバーを消す

            // var newPage = new NavigationPage(new MainPageMenu());
            //var newPage = new ContentPage();
            //Navigation.PushAsync(newPage);
            //var poppedPage = Navigation.PopAsync();

        }


        private void OnRankingTap(object sender, EventArgs e)
        {
            var newPage = new Ranking();
            Navigation.PushAsync(newPage);
            //var str = ((Label)sender).Text;
            //DisplayAlert("Tapped", str + " is Tapped", "OK");

        }


        public void OnInvestTap(object sender, EventArgs e)
        {
            var newPage = new ListViewPage();
            Navigation.PushAsync(newPage);
            //var poppedPage = Navigation.PopAsync();

            //DisplayAlert("Seleted", ((ToolbarItem)sender).Name, "OK");
        }

    }
}
