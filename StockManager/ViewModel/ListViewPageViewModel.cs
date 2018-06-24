using PCLStorage.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;


using Xamarin.Forms;



namespace StockManager
{
    /// <summary>
    /// ListView を表示する View の ViewModel
    /// </summary>
    class ListViewPageViewModel : BindableBase// ViewModelBase //INotifyPropertyChanged
    {
        //public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// View への参照
        /// </summary>
        public ListViewPage View { get; set; }
       

        public ICommand SelectCommand { private set; get; }
        public ICommand ModelOnLabel { get; }
        /// <summary>
        /// ListView の各 Item 内の Button にバインディングする Command
        /// </summary>
        public ICommand ItemCommand { protected set; get; }
        public ICommand RefCommand { protected set; get; }
        public ICommand DelAddCommand { set; get; }
        //public ICommand EditTapp {set; get; }
        // LiseViewのButtonにバインディングするCommand
        // ListViewを引っ張った時に実行させるコマンド
        public ICommand AddCommand { get; private set; }

        //public ObservableCollection<Price> ItemList { get; set; }
        //public ObservableCollection<PageViewModel> ItemList { get; set; }

        #region  OnPropertyChanged
        private static decimal _payAssetprice;
        public decimal PayAssetprice//保有数* 購入価格 = 投資総額
        {
            get { return _payAssetprice; }
            set
            {
                _payAssetprice = value;
                this.OnPropertyChanged(nameof(PayAssetprice));
            }
        }

        public static decimal _totalAsset;
        public decimal TotalAsset //現在評価額合計
        {
            get { return _totalAsset; }
            set
            {
                _totalAsset = value;
                this.OnPropertyChanged(nameof(TotalAsset));
            }

        }

        private static string _polar;
        public string Polar//(-)下落
        {
            get { return _polar; }
            set
            {
                _polar = value;
                // StockMvvmView.ChangeButtonColor(_polar);
                this.OnPropertyChanged(nameof(Polar));
            }
        }


        private static decimal _uptoAsset;
        public decimal UptoAsset
        {
            get { return _uptoAsset; }
            set
            {
                _uptoAsset = value;
                this.OnPropertyChanged(nameof(UptoAsset));
            }
        }




        private static string inputString = "";
        // Public properties
        public string InputString
        {
            get { return inputString; }
            set
            {
                if (inputString != value)
                {
                    inputString = value;
                }
                inputString = value;
                this.OnPropertyChanged(nameof(InputString));
            }
        }


        public static string _percent;
        public string Percent
        {
            get { return _percent; }
            set
            {
                if (_percent != value)
                {
                    _percent = value;
                }
                _percent = value;
                this.OnPropertyChanged(nameof(Percent));
            }
        }


        public static string _prev_day;
        public string Prev_day
        {
            get { return _prev_day; }
            set
            {
                if (_prev_day != value)
                {
                    _prev_day = value;
                }
                _prev_day = value;
                this.OnPropertyChanged(nameof(Prev_day));
            }
        }



        public static string _realprice;
        public string Realprice
        {
            get { return _realprice; }
            set
            {
                if (_realprice != value)
                {
                    _realprice = value;
                }
                _realprice = value;
                this.OnPropertyChanged(nameof(Realprice));
            }
        }



        //Ni255

        public static string _Ni255percent;
        public string Ni255Percent
        {
            get { return _Ni255percent; }
            set
            {
                if (_Ni255percent != value)
                {
                    _Ni255percent = value;
                }
                _Ni255percent = value;
                this.OnPropertyChanged(nameof(Ni255Percent));
            }
        }




        public static string _Ni255prev_day;
        public string Ni255Prev_day
        {
            get { return _Ni255prev_day; }
            set
            {
                if (_Ni255prev_day != value)
                {
                    _Ni255prev_day = value;
                }
                _Ni255prev_day = value;
                this.OnPropertyChanged(nameof(Ni255Prev_day));
            }
        }



        public static string _Ni255realprice;
        public string Ni255Realprice
        {
            get { return _Ni255realprice; }
            set
            {
                if (_Ni255realprice != value)
                {
                    _Ni255realprice = value;
                }
                _Ni255realprice = value;
                this.OnPropertyChanged(nameof(Ni255Realprice));
            }
        }



        public static string _firstLastName;
        public string FirstLastName
        {
            get { return _firstLastName; }
            set
            {
                if (_firstLastName != value)
                {
                    _firstLastName = value;
                }
                _firstLastName = value;
                this.OnPropertyChanged(nameof(FirstLastName));
            }
        }


        public static string _Ni255firstLastName;
        public string Ni255FirstLastName
        {
            get { return _Ni255firstLastName; }
            set
            {
                if (_Ni255firstLastName != value)
                {
                    _Ni255firstLastName = value;
                }
                _Ni255firstLastName = value;
                this.OnPropertyChanged(nameof(Ni255FirstLastName));
            }
        }


        private static string _Ni255polar;
        public string Ni255Polar//(-)下落
        {
            get { return _Ni255polar; }
            set
            {
                _Ni255polar = value;
                // StockMvvmView.ChangeButtonColor(_polar);
                this.OnPropertyChanged(nameof(Ni255Polar));
            }
        }


        private ObservableCollection<PageViewModel> _itemList;

        public ObservableCollection<PageViewModel> ItemList
        {
            get
            {
                return this._itemList;
            }
            set
            {
                this.SetProperty(ref this._itemList, value);
            }
        }




        #endregion



        /// <summary>
        /// デフォルト コンストラクタ
        /// </summary>
        public ListViewPageViewModel()
        {

           
            //ItemList = new ObservableCollection<PageViewModel>();//初期化
            RefCommand = new CountUpCommand(IncrementData);
            //DelAddCommand = new CountUpCommand(DelAdd);
            DelAddCommand = new DelegateCommand<PageViewModel>(DelAdd);
            SelectCommand = new DelegateCommand<object>(Selecter);
            AddCommand = new CountUpCommand(DataAdd);

            ItemCommand = new Command<string>((key) =>
            {
                // Add the key to the input string.
                //InputString += key;
                OnItemCommand(key);
            });


            StdStock();
            //DispSet(false);
            Selecter("Page1");//スタートアップ Stock 画面
            //Selecter("Page2");//スタートアップ Ranking 画面


        }




        #region メソッド
        public  void Selecter(object sender)
        {
            //var SourceBool = (string)sender.CommandParameter;//FirstLastName (Prev_day or Percent)
            ItemList = new ObservableCollection<PageViewModel>();//初期化
            //PageViewModel[] PageViewModels;

            var disp = Convert.ToString(sender);

            if (disp == "Page1")//Page1
            {
                //this.ItemList = new ObservableCollection<PageViewModel>{
                //    new Page1ViewModel{ Name = "Page1"},
                //};
                 DispSet(false);


            }
            else//Page2
            {
                SetRankingTabledata();

                //this.ItemList = new ObservableCollection<PageViewModel>{
                   // new Page2ViewModel{ Title2 = "Item1"},
                   // new Page2ViewModel{ Title2 = "Item2"},
                   // new Page2ViewModel{ Title2 = "Item3"},
                   // new Page2ViewModel{ Title2 = "Item4"},
                   // new Page2ViewModel{ Title2 = "Item5"},
                   //};
              

            }

        }







        public static void DataSave(int ID, string savedata)
        {
            //Models Model = new Models();
            //private async void OnLabelClicked()
            Models.DataStorage(ID, savedata);

        }



        public void DataAdd()
        {
            View.Navigation.PushAsync(new AddPage());

            //Models.DataStorageAdd(savedata );

        }

        public void DelAdd(PageViewModel parameter)
        {
            var itemindex = (parameter).Idindex;
            Models.Deleet(itemindex);
            View.DisplayAlert("DelCommand Context Action", itemindex + " Delete complete", "OK");
        }






        private async void StdStock()
        {
            Price IndnAnser = await Models.Getserchi("^DJI");
            Price Ni255Anser = await Models.Getserchi("998407");


            // Name = IndnAnser.Name,
            Prev_day = IndnAnser.Prev_day;//前日比±**
            Percent = IndnAnser.Percent;//前日比％**// "5"
            Realprice = Convert.ToString(IndnAnser.Realprice) + "   " + IndnAnser.Prev_day + "   " + IndnAnser.Percent;//現在値*
            FirstLastName = IndnAnser.FirstLastName;
            Polar = IndnAnser.Polar;

            //Itemprice = Ni255Anser.Itemprice;// 2015,
            Ni255Prev_day = Ni255Anser.Prev_day;
            Ni255Percent = Ni255Anser.Percent;//前日比％**// "5"
            Ni255Realprice = Convert.ToString(Ni255Anser.Realprice) + "   " + Ni255Anser.Prev_day + "   " + Ni255Anser.Percent;//現在値*
            Ni255FirstLastName = Ni255Anser.FirstLastName;
            Ni255Polar = Ni255Anser.Polar;

        }


        #endregion


        /// <summary>
        /// ListView の Button 押下時の動作
        /// </summary>
        /// <param name="parameter"></param>
        private void OnItemCommand(string key)
        {
            View.DisplayAlert("XSample", "SelectItem-" + key, "OK");

            var index = Convert.ToInt32(key);


            //SettersExtensions(index, ItemList[0].Percent);
        }



        //private void Sample()
        //{
        //    ItemList.Add(new Page1ViewModel
        //    {
        //        Name = "SampleSony",
        //        Stocks = 100,
        //        Itemprice = 2015,
        //        Realprice = 1000,
        //        RealValue = 100,
        //        Percent = "5"
        //    });



        //}







        public async void DispSet(bool Refresh)
        {
            int i = 0;
            decimal TotalAssetAdd = 0;
            decimal PayAssetpriceAdd = 0;

            try
            {
                // UTF8のファイルの書き込み Edit. 
                //string write = await StorageControl.PCLSaveCommand("6758,200,1665\n9837,200,712\n6976,200,1846\n6502,0,0");//登録データ書き込み
                // List<Price> prices = Finance.Parse(await StorageControl.PCLLoadCommand());//登録データ読み込み
                List<Price> pricesanser = await Models.PasonalGetserchi();//登録データの現在値を取得する
            

                if (Refresh == true)
                {
                    ItemList.Clear();// 全て削除
                }

                foreach (Price item in pricesanser)
                {
                    ItemList.Add(new Page1ViewModel
                    {
                        Name = item.Name,// "Sony",
                        Stocks = item.Stocks,//保有数*
                        Itemprice = item.Itemprice,//
                        Prev_day = item.Prev_day,//前日比±**
                        Realprice = item.Realprice,//現在値*// 1000,
                        RealValue = item.RealValue,// 1
                        Idindex = i,
                        // ButtonColor = item.Polar,
                        Polar = item.Polar,
                        FirstLastName = item.FirstLastName,
                        Percent = item.Percent,//前日比％**// "5"
                        Gain = item.Gain,//損益Name
                    });


                    PayAssetpriceAdd = PayAssetpriceAdd + (pricesanser[i].Stocks * Convert.ToDecimal(pricesanser[i].Itemprice));//株数*購入単価の合計
                    TotalAssetAdd = TotalAssetAdd + (pricesanser[i].Stocks * Convert.ToDecimal(pricesanser[i].Realprice));//現在評価額
                                                                                                                          //Gain = item.Realprice - item.Itemprice;
                    i = ++i;
                }
                PayAssetprice = PayAssetpriceAdd;
                TotalAsset = TotalAssetAdd;
                UptoAsset = TotalAsset - PayAssetprice;
            }
            catch (System.IO.FileNotFoundException)
            {
                await View.DisplayAlert("登録データがありません", "Add.ボタンを押して登録して下さい。", "OK");
                await StorageControl.PCLSaveCommand("");//新規ファイル登録書き込み
            }
        }

        public async void RefDispSet()
        {
            int i = 0;
            decimal TotalAssetAdd = 0;
            decimal PayAssetpriceAdd = 0;


           
            // UTF8のファイルの書き込み Edit. 
            string write = await StorageControl.PCLSaveCommand("6758,200,1665\n9837,200,712\n6976,200,1846");//登録データ書き込み
            // List<Price> prices = Finance.Parse(await StorageControl.PCLLoadCommand());//登録データ読み込み
            List<Price> priceanser = await Models.PasonalGetserchi();//登録データの現在値を取得する


            foreach (Price item in priceanser)
            {
                ItemList.Add(new Page1ViewModel
                {
                    Name = item.Name,// "Sony",
                    Stocks = item.Stocks,//保有数*
                    Itemprice = item.Itemprice,// 2015,
                    Prev_day = item.Prev_day,//前日比±**
                    Realprice = item.Realprice,//現在値*// 1000,
                    RealValue = item.RealValue,// 100,
                    Percent = item.Percent,//前日比％**// "5"
                   
                });
                PayAssetpriceAdd = PayAssetpriceAdd + (priceanser[i].Stocks * Convert.ToDecimal(priceanser[i].Itemprice));//株数*購入単価の合計
                TotalAssetAdd = TotalAssetAdd + (priceanser[i].Stocks * Convert.ToDecimal(priceanser[i].Realprice));//現在評価額

                i = ++i;
            }

            PayAssetprice = PayAssetpriceAdd;
            TotalAsset = TotalAssetAdd;
            UptoAsset = TotalAsset - PayAssetprice;

        }




        public async void SetRankingTabledata()
        {
            int i = 0;
            decimal TotalAssetAdd = 0;
            decimal PayAssetpriceAdd = 0;



           
            // List<Price> prices = Finance.Parse(await StorageControl.PCLLoadCommand());//登録データ読み込み
            Tabledata anser = await Models.RankingTabledata();//登録データの現在値を取得する


          
                ItemList.Add(new Page1ViewModel
                {
                    //RankingValue = anser.RankingValue,
                    Name = anser.Name,// "Sony",
                    //Stocks = anser.Stocks,//保有数*
                    //Itemprice = anser.Itemprice,// 2015,
                    //Prev_day = anser.Prev_day,//前日比±**
                    //Realprice = anser.Realprice,//現在値*// 1000,
                    //RealValue = anser.RealValue,// 100,
                    //Percent = anser.Percent,//前日比％**// "5"

                });
               

                i = ++i;


            PayAssetprice = PayAssetpriceAdd;
            TotalAsset = TotalAssetAdd;
            UptoAsset = TotalAsset - PayAssetprice;




        }





        public void IncrementData()
        {
            View.Reloadbusy(true);

            StdStock();
            DispSet(true);

            View.Reloadbusy(false);
        }
        
    }

    public class ButtonClickedTriggerAction : TriggerAction<Button>
    { 
        protected override void Invoke(Button sender)
        { 
            var SourceWord = (string)sender.CommandParameter;//FirstLastName (Prev_day or Percent)
            int index = SourceWord.LastIndexOf(",");
            var PercentWord = SourceWord.Substring(index + 1, (SourceWord.Length - index) - 1);

            index = SourceWord.LastIndexOf(",");
            var Prev_dayWord = SourceWord.Substring(index - index,index);

            if (sender.Text == Prev_dayWord)
            {
                sender.Text = PercentWord;
            }
            else
            { 
                sender.Text = Prev_dayWord;
            }
        }
    }

   



}

/*------------------------- Binding Sample -------------------------------------- 
//var IndnButton = new Button { BackgroundColor = Color.Brown };

//Binding binding = new Binding(nameof(IndnAnser.Polar)) { Source = IndnAnser };
//IndnButton.SetBinding(Button.BackgroundColorProperty, binding);


//// Changes source value.
//IndnAnser.ButtonColor = "Red";



//// Binding Target (DependencyObject).
//var Goingprice = new Label { Text = "Default" };


//// Binds target to source.
//var binding1 = new Binding(nameof(IndnAnser.Realprice)) { Source = IndnAnser };
//Goingprice.SetBinding(Label.TextProperty, binding1);


// Changes source value.
//IndnAnser.Realprice = 99999;




//XAML
//< TextBox Height = "24"  Width = "120" Text = "{Binding Path=Name}" />

//C#Code
////設定
//Binding binding = new Binding("Name");
//textBox1.SetBinding(TextBox.TextProperty, binding);





 //async void OnDoThisClick(object sender, EventArgs ea)
        //{
        //    await DisplayAlert("Do this", "alert displays as well as TriggerAction", "Cool");
        //}

        //async void OnDoThatClick(object sender, EventArgs ea)
        //{
        //    await DisplayAlert("Do that", "alert displays as well as TriggerAction", "Cool");
        //}
       

        //public void OnEdit(object sender, EventArgs e)
        //{
           // this.Navigation.PushAsync(new EntryPage(e));

            //var newPage = new ContentPage();
            //Navigation.PushAsync(newPage);
           
            //var poppedPage = Navigation.PopAsync();
          

            //var usercode = new Entry { Placeholder = "Code", Keyboard = Keyboard.Text, };

          

            //mi.CommandParameter as ContactHistoryItem

            //MenuItem mi = ((MenuItem)sender);
            //var par = mi.CommandParameter;
            //var selectedText =  this.DisplayActionSheet("Edit", "Close", "Chancel", new string[] {"qqqq", "すいか", "ぶどう"});
           
            //if (selectedText != null)
            //{
            //    //buttonDialog2.Text = selectedText;
            //}

            //DisplayAlert("Edit Context Action", e.ToString() + " edit context action", "OK");

            //ListViewPageViewModel.OnLabelClicked(mi);


       // }

























----------------------------------------------------------------------------------*/
