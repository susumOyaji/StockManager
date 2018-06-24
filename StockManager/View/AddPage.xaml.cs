using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace StockManager
{
    public partial class AddPage : ContentPage
    {
        public AddPage()
        {
            InitializeComponent();
            AddEntryPage();
        }




        //NavigationPage.SetHasNavigationBar(this, false);

        internal async void AddEntryPage()
        {
            var listdata = await StorageControl.PCLLoadCommand();//登録データ読み込み
            // カンマ区切りで分割して配列に格納する
            string[] stArrayData = listdata.Split('\n');

            var id = stArrayData.Length;

            Entry usercode = code;
            Entry usercost = stock;
            Entry usershares = price;

                      


            ToolbarItems.Add(new ToolbarItem
            { // <-2
                Text = "Cansel", // <-3
                Command = new Command(() => Navigation.PopAsync())
            });


            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Add.",
                Command = new Command(() => AddData( usercode, usercost, usershares, stArrayData))
            });



        }


        public void AddData( Entry code, Entry cost, Entry shares, string[] std)
        {
            if (code.Text != null && cost.Text != null && shares.Text != null)
            {
                string SaveData = code.Text + "," + cost.Text + "," + shares.Text;

                std.CopyTo(std = new string[std.Length + 1], 0);
                std[std.Length - 1] = SaveData;

                Models.DataStorageAdd(std);

                Navigation.PopAsync();


            }
            else
            {
                DisplayAlert("Add Command Meseg", "未入力の項目があります。", "OK");
            }
        }

    }
}
