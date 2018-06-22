using System;
using StockManager.View;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Collections.Generic;

namespace StockManager
{
    public partial class ListViewPage : ContentPage
    {
        // internal ListViewPageViewModel Vm { get; set; }
        private ObservableCollection<string> messages = new ObservableCollection<string>();

       
        public ListViewPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);//1st.Pageのバーを消す

        }



        public void Reloadbusy(bool rotation)
        {
           // Indicator.IsEnabled = rotation;
            Indicator.IsVisible = rotation; // true 回転, faluse 停止
           
        }





        public void OnEdit(object sender, EventArgs e)
        {
            
            var newPage = new ContentPage();
            Navigation.PushAsync(newPage);

            var poppedPage = Navigation.PopAsync();


            var usercode = new Entry { Placeholder = "Code", Keyboard = Keyboard.Text, };



            //mi.CommandParameter as ContactHistoryItem

            MenuItem mi = ((MenuItem)sender);
            var par = mi.CommandParameter;
            var selectedText = this.DisplayActionSheet("Edit", "Close", "Chancel", new string[] { "qqqq", "すいか", "ぶどう" });

            if (selectedText != null)
            {
                //buttonDialog2.Text = selectedText;
            }

            DisplayAlert("Edit Context Action", e.ToString() + " edit context action", "OK");

            //ListViewPageViewModel.OnLabelClicked(mi);


        }


        internal void CanselCommand(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new EntryPage(sender, e));
            var mi = ((MenuItem)sender);

            //var itemindex = mi.CommandParameter;
            var itemindex = ((Price)mi.CommandParameter).Idindex;
            DisplayAlert("CanselCOmmand Context Action", itemindex + " cansel context action", "OK");
        }



        //void OnSendTapped(object sender, EventArgs args)
        //{
        //    //RankingListview.ItemsSource = new string[]{
        //    //                                "mono",
        //    //                                "monodroid",
        //    //                                "monotouch",
        //    //                                "monorail",
        //    //                                "monodevelop",
        //    //                                "monotone",
        //    //                                "monopoly",
        //    //                                "monomodal",
        //    //                                "mononucleosis"
        //    //                            };

        //}


        //public class MyDataTemplateSelector : DataTemplateSelector
        //{
        //    private readonly DataTemplate defaulTemplate;
        //    private readonly Dictionary<Type, DataTemplate> templateSet;

        //    public MyDataTemplateSelector(Dictionary<Type, DataTemplate> templateSet)
        //    {
        //        this.templateSet = templateSet;

        //        defaulTemplate = new DataTemplate(typeof(TextCell));
        //        defaulTemplate.SetBinding(TextCell.TextProperty, new Binding(".", stringFormat: "{0}"));
        //    }

        //    #region implemented abstract members of DataTemplateSelector
        //    // itemにはセルのデータ、containerにはセルの親(ListViewやTableView)が渡される
        //    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        //    {
        //        DataTemplate template;
        //        if (templateSet != null &&
        //            templateSet.TryGetValue(item.GetType(), out template))
        //        {
        //            return template;
        //        }

        //        return defaulTemplate;
        //    }
        //    #endregion
        //}


        //public App()
        //{
        //    var colorTemplate = new DataTemplate(typeof(TextCell));
        //    colorTemplate.SetBinding(TextCell.TextProperty, new Binding(".", stringFormat: "Color:{0}"));
        //    colorTemplate.SetBinding(TextCell.TextColorProperty, ".");

        //    var boolTemplate = new DataTemplate(typeof(SwitchCell));
        //    boolTemplate.SetBinding(SwitchCell.OnProperty, ".");

        //    var templateSelector = new MyDataTemplateSelector(
        //        new Dictionary<Type, DataTemplate> {
        //    {typeof(Color), colorTemplate},
        //    {typeof(bool), boolTemplate},
        //    });

        //    MainPage = new ContentPage
        //    {
        //        Content = new ListView
        //        {
        //            ItemTemplate = templateSelector,
        //            ItemsSource = new object[] {
        //        Color.Blue,
        //        false,
        //        true,
        //        Color.Red,
        //        Color.Teal,
        //        true,
        //        "Hello World", // ここからはデフォルトテンプレートの確認用
        //        1,
        //        33.4,
        //    },
        //        }
        //    };
        //}



        internal void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var mi = ((MenuItem)sender);
            var option = mi.Text;

            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            DisplayAlert("Item Selected", e.SelectedItem.ToString(), "Ok");
            //((ListViewPage)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.

        }


        /// <summary>
        /// ListViewの項目選択時に呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EntryPage(sender, e));
      
        }


        /// <summary>
        /// 項目タップ時に呼ばれる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //this.Navigation.PushAsync(new EntryPage(e));
            //Navigation.PopAsync();
            //DisplayAlert("Item Tapped", item.ToString(), "Ok");

        }

    }

}
