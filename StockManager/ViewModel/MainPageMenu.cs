using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using Xamarin.Forms;
using StockManager;

namespace StockManager
{
    /// <summary>
    /// MainPage を表示する View の ViewModel
    /// </summary>
    public partial class MainPageMenu :ViewModelBase// INotifyPropertyChanged
    {
        /// <summary>
        /// View への参照
        /// </summary>
        public MainPage View { get; set; }

        private ICommand topInvestment;
        ICommand topRanking;
        ICommand topOption;

       
        /// <summary>
        /// Initializes a new instance of the <see cref="T:StockManagement.Menu"/> class.
        /// </summary>
        public MainPageMenu()
        {
            // configure the TapCommand with a method
            topInvestment = new  Command(OnInvest);
            topRanking = new Command(OnRanking);
            topOption = new Command(OnOption);

        }

        /// <summary>
        /// Gets the top investment.
        /// </summary>
        /// <value>The top investment.</value>
        public ICommand TopInvestment//Binding Name
        {
            get { return topInvestment; }
        }

        public ICommand TopRanking
        { 
            get { return topRanking; }
        }

        public ICommand TopOption
        {
            get { return topOption; }
        }

        public object Navigation { get; private set; }

        void OnInvest(object s)
        {
           
           // var Page = new NavigationPage(new ListViewPage()); //MainPage = new NavigationPage(new MainPage()); MainPage = new StockMvvmPage();
            //var newPage = new ContentPage();
            //Navigation.PushAsync(newPage);
            // View.DisplayAlert("coming soon", "SelectItem-" + Page, "OK");
        }

        void OnRanking(object s)
        {

            var Page = new Ranking(); //MainPage = new NavigationPage(new MainPage()); MainPage = new StockMvvmPage();

            View.DisplayAlert("coming soon", "SelectItem-" + Page, "OK");
        }

        void OnOption(object s)
        {

            var Page = new Ranking(); //MainPage = new NavigationPage(new MainPage()); MainPage = new StockMvvmPage();

            View.DisplayAlert("coming soon", "SelectItem-" + s, "OK");
        }

    }
}
