using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StockManager.View
{
    public partial class EntryPage1 : ContentPage
    {
        public EntryPage1(object sender, SelectedItemChangedEventArgs e)
        {
            InitializeComponent();

            var itemindex = ((PageViewModel)e.SelectedItem).Idindex;
            var itemname = ((PageViewModel)e.SelectedItem).Name;
            var itemstock = ((PageViewModel)e.SelectedItem).Stocks;
            var itemprice = ((PageViewModel)e.SelectedItem).Itemprice;


            //NavigationPage.SetHasNavigationBar(this, false);
            var ID = new Label { Text = itemindex.ToString(), BackgroundColor = Color.Gray, TextColor = Color.White };
            index.Text = itemindex.ToString();
            code.Text = itemname;
            stock.Text = itemstock.ToString();
            price.Text = itemprice.ToString();




            ToolbarItems.Add(new ToolbarItem
            { // <-2
                Text = "Cansel", // <-3
                Command = new Command(() => Navigation.PopAsync())
            });

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Save",
                Command = new Command(() => SetData(itemindex, index, code, price))
            });

        }

        void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void Entry_Completed(object sender, EventArgs e)
        {

        }

        public void SetData(int index, Entry code, Entry cost, Entry shares)
        {
            if (code.Text != "" && cost.Text != "" && shares.Text != "")
            {
                string SaveData = code.Text + "," + cost.Text + "," + shares.Text;
                ListViewPageViewModel.DataSave(index, SaveData);
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Entry Command Meseg", "未入力の項目があります。", "OK");
            }

        }





    }
}
