using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockManager.View
{
    public partial class IndicatorView : ContentView
    {
        public IndicatorView()
        {
            InitializeComponent();


            button.SizeChanged += (sender, e) =>
            {
                button.TranslationX = -(button.Width / 2);
                button.TranslationY = -(button.Height / 2);
            };
        }
    }
}
