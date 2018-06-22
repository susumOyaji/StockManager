using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace StockManager
{
    public partial class Ranking : ContentPage
    {
        private ObservableCollection<string> messages = new ObservableCollection<string>();

        public Ranking()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);//1st.Pageのバーを消す

        }


    }
}
