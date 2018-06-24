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
            //var ID = new Label { Text = itemindex.ToString(), BackgroundColor = Color.Gray, TextColor = Color.White };
            //現行データを表示する
            index.Text = "登録順位   " + itemindex.ToString();
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
                Command = new Command(() => SetData(itemindex, code, stock, price))
            });

        }

        void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void Entry_Completed(object sender, EventArgs e)
        {
            
        }

        public void SetData(int index, Entry code, Entry stock, Entry price)
        {
            if (code.Text != "" && stock.Text != "" && price.Text != "")
            {
                string SaveData = code.Text + "," + stock.Text + "," + price.Text;
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
