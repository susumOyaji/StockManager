using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Indicator : ContentPage
    {
        public Indicator()
        {
            InitializeComponent();
        }


        async Task Guruguru( )
        {

            try
            {
                cvLayer.IsVisible = true;
                frLayer.IsVisible = true;
                Indicator.IsRunning = true;

                await Task.Run(() =>
                {
                    //長い処理を記述
                });
            }
            finally
            {
                Indicator.IsRunning = false;
                frLayer.IsVisible = false;
                cvLayer.IsVisible = false;
            }

        }
    }
}