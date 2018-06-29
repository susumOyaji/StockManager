using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Globalization;

//using StockMvvm.ViewModels;



namespace StockManager
{

    public class Models : ContentPage
    {

        static void ModelS() { }//MainModel to end

        internal static int Incriment(int number)
        {
            return number + 1;
            //DateTime time = DateTime.Now;//new System.DateTime("yyyy", 1, 1, 0, 0, 0, 0);
        }














        /// <summary>
        /// Getserchi the specified code.
        /// </summary>
        /// <param name="code">Code.</param>
        public static async Task<Price> Getserchi(string code)
        {
            string Value = null;
            string ValueRatio = null;
            string PercentRatio = null;

            Price cityprice = new Price();

            string url = "http://stocks.finance.yahoo.co.jp/stocks/detail/?code=" + code;// +".T";

            var httpClient = new HttpClient();
            string str = await httpClient.GetStringAsync(url);

            string searchWord = "stoksPrice";    //検索する文字列 ="stoksPrice"> 
            int foundIndex = str.IndexOf(searchWord);//始めの位置を探す
                                                     //次の検索開始位置
            int nextIndex = foundIndex + searchWord.Length;
            try
            {
                //次の位置を探す
                foundIndex = str.IndexOf(searchWord, nextIndex);
                if (foundIndex != -1)
                {
                    int i = searchWord.Length + 2;//pricedata to point
                    for (; Convert.ToString(str[foundIndex + i]) != "<"; i++)
                    {
                        Value = Value + str[foundIndex + i];//current value 現在値
                    }
                }
                else
                {
                    cityprice.Realprice = 0;// "Error";
                }

                string searchWord1 = "yjMSt"; //検索する文字列前日比
                int foundIndex1 = str.IndexOf(searchWord1);//始めの位置を探す
                int i1 = searchWord1.Length + 2;

                for (; Convert.ToString(str[foundIndex1 + i1]) != "（"; i1++)
                {
                    ValueRatio = ValueRatio + str[foundIndex1 + i1];//previous 前日比? ¥
                }

                if (Convert.ToString(str[foundIndex1 + i1 + 1]) == "-")//(－)下落
                {
                    cityprice.Polar = "Green";
                }
                else
                {
                    cityprice.Polar = "Red";
                }


                i1++;
                for (; Convert.ToString(str[foundIndex1 + i1]) != "）"; i1++)
                {
                    PercentRatio = PercentRatio + str[foundIndex1 + i1];//previous 前日比? %
                }
                cityprice.Name = code;
                cityprice.Realprice = Convert.ToDecimal(Value);//現在値 ***
                cityprice.Prev_day = ValueRatio;//前日比± ***
                cityprice.Percent = PercentRatio; //前日比％ ***


            }
            catch (Exception)
            {
                cityprice.Percent = "Close"; //前日比％
                cityprice.Polar = "Gray";

            }
            return cityprice;// polarity;
        }










        ///
        /// <summary>
        /// Pasonals the getserchi.
        /// </summary>
        /// <returns>The getserchi.</returns>
        public static async Task<List<Price>> PasonalGetserchi()//企業名を設定して現在値を取得する
        {
            string Value = null;
            string YenRatio = null;
            string PercentRatio = null;
            int index = 0;

            // UTF8のファイルの読み込み Edit.        
            string responce = await StorageControl.PCLLoadCommand();//登録データ読み込み

            List<Price> prices = Finance.Parse(responce);

            foreach (Price price in prices)
            {
                string url = "http://stocks.finance.yahoo.co.jp/stocks/detail/?code=" + price.Name;// +".T";

                var httpClient = new HttpClient();
                string str = await httpClient.GetStringAsync(url);

                string searchWord = "stoksPrice";    //検索する文字列 ="stoksPrice"> 
                int foundIndex = str.IndexOf(searchWord);//始めの位置を探す
                                                         //次の検索開始位置
                int nextIndex = foundIndex + searchWord.Length;
                try
                {
                    //次の位置を探す
                    foundIndex = str.IndexOf(searchWord, nextIndex);
                    if (foundIndex != -1)
                    {
                        int i = searchWord.Length + 2;//pricedata to point
                        for (; Convert.ToString(str[foundIndex + i]) != "<"; i++)
                        {
                            Value = Value + str[foundIndex + i];//current value 現在値
                        }
                    }
                    else
                    {
                        //price[0] = "Error";
                    }

                    string searchWord1 = "yjMSt"; //検索する文字列前日比
                    int foundIndex1 = str.IndexOf(searchWord1);//始めの位置を探す
                    int i1 = searchWord1.Length + 2;

                    for (; Convert.ToString(str[foundIndex1 + i1]) != "（"; i1++)
                    {
                        YenRatio = YenRatio + str[foundIndex1 + i1];//previous 前日比? ¥
                    }

                    if (Convert.ToString(str[foundIndex1 + i1 + 1]) == "-")//(－)下落
                    {
                        price.Polar = "Green";//(-)
                    }
                    else
                    {
                        price.Polar = "Red";//(+)
                    }


                    i1++;
                    for (; Convert.ToString(str[foundIndex1 + i1]) != "）"; i1++)
                    {
                        PercentRatio = PercentRatio + str[foundIndex1 + i1];//previous 前日比? %
                    }


                    if (Value == "---")
                    {
                        price.Realprice = 000;
                    }
                    else
                    {
                        price.Realprice = Convert.ToDecimal(Value);//現在値
                    }
                    price.Prev_day = YenRatio;//前日比±
                    price.Percent = PercentRatio; //前日比％
                    price.PayAssetprice = price.Stocks * price.Itemprice;//株数*購入単価
                    price.Gain = (price.Realprice - price.Itemprice) * price.Stocks;//損益
                    price.RealValue = (price.Stocks * price.Realprice);//個別利益


                    Value = "";
                    YenRatio = "";
                    PercentRatio = "";
                    index = index + 1;
                }
                catch (Exception)
                {
                    price.Prev_day = "Close";
                    price.Polar = "Gray";
                }
            }

            return prices;//polarity;

        }//class to end 






        /// <summary>
        /// Ons the label clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public async static void DataStorage(int ID, string sender)
        {

            //List<string> listdata = new List<string>();
            var listdata = await StorageControl.PCLLoadCommand();//登録データ読み込み

            // カンマ区切りで分割して配列に格納する
            string[] stArrayData = listdata.Split('\n');
            stArrayData[ID] = sender;

            //// 配列内のデータをすべてカンマ区切りで連結する
            string CsvData = string.Join("\n", stArrayData);
                     
            // UTF8のファイルの書き込み Edit. 
            string write = await StorageControl.PCLSaveCommand(CsvData);//登録データ書き込み
          



            //string Usercost = usercost.Text;
            //string Usershares = usershares.Text;
            //string[] cols;
            ////string[] savedata;
            //string[] entrydata = new string[3];
            //string[] mergedArray = entrydata;//連結データ
            //string[] returncode = new string[] { "\n" };


            //    string response = await saveLoadCS.DataLoadAsync();//.Replace("\r", "").Split('\n');
            //    string[] rows = response.Split('\r');
            //    cols = rows;
            //    entrydata[0] = Usercode;//usercode.Text = "Code or Cost or Usershares failed";
            //    entrydata[1] = Usercost;
            //    entrydata[2] = Usershares;

            //    if (response != "")
            //    {
            //        foreach (string row in rows)
            //        {
            //            if (string.IsNullOrEmpty(row)) continue;
            //            cols = row.Split(',');
            //        }
            //        //Concatで連結して、ToArrayで配列にする
            //        mergedArray = cols.Concat(entrydata).ToArray();
            //    }

            //}
            //// 配列内のデータをすべてカンマ区切りで連結する
            //string CsvData = string.Join(",", mergedArray);
            //await saveLoadCS.DataSaveAsync(CsvData + "\n");
            //await DisplayAlert("SAVE", " is InputData", "OK");
            ////InsertRange メソッド

        }


        public async static void DataStorageAdd(string[] Adddata )
        {

            //// カンマ区切りで分割して配列に格納する
            //string[] stArrayData = listdata.Split('\n');
            ////stArrayData[ID] = sender;

            ////// 配列内のデータをすべてカンマ区切りで連結する
            string CsvData = string.Join("\n", Adddata);

            //// UTF8のファイルの書き込み Edit. 
            string write = await StorageControl.PCLSaveCommand(CsvData);//登録データ書き込み

        }



        /// <summary>
        /// Ons the label clicked.
        /// </summary>
        /// <param name="ID"></param>
        public async static void Deleet(int ID )
        {

            //List<string> listdata = new List<string>();
            var listdata = await StorageControl.PCLLoadCommand();//登録データ読み込み

            // カンマ区切りで分割して配列に格納する
            string[] stArrayData = listdata.Split('\n');
            stArrayData[ID] = null;//削除行をnullで消す

            int i = 0;


            foreach (string row in stArrayData)
            {
                if (string.IsNullOrEmpty(row) != true)//null行を読み飛ばす
                {
                    stArrayData[i] = row;
                    i++;
                }
            }
            stArrayData[i] = "null";//最下行を消去する

            //要素数5の配列
            //int[] intArray = { 0, 1, 2, 3, 4 };
            //要素数を10に増やす
            Array.Resize(ref stArrayData, i);




            //// 配列内のデータをすべて\n区切りで連結する
            string CsvData = string.Join("\n", stArrayData);

            // UTF8のファイルの書き込み Edit. 
            string write = await StorageControl.PCLSaveCommand(CsvData);//登録データ書き込み


        }







  
        public static async Task<Tabledata> RankingTabledata()
        {
            int index = 0;
            int i = 0;
            //Decimal PayAssetprice = 0;
            //Decimal TotalAssetprice = 0;
            //string NameMulti = "";
            //string TrgetWord = null, TrgetWord1 = null, TrgetWord2 = null;
            string RankingValue = null;
            string Name = null;
            string Dividend= null;
          



            string url = "https://info.finance.yahoo.co.jp/ranking/?kd=8&tm=d&vl=a&mk=3&p=1";
            var httpClient = new HttpClient();
            string str = await httpClient.GetStringAsync(url);
           
            Tabledata tabledata = new Tabledata();



          
                string searchWord = "rankingTabledata yjM";    //検索する文字列 ="stoksPrice"> 
                string RankWore = "txtcenter\">";

                int foundIndex = str.IndexOf(searchWord);//始めの位置を探す

                //Rankingの検索開始位置
                int RankIndex = foundIndex + searchWord.Length;
                try
                {
                    //次の位置を探す
                    foundIndex = str.IndexOf(RankWore, RankIndex);

                for (; Convert.ToString(str[foundIndex+ RankWore.Length + i]) != "<"; i++)
                {
                    if (foundIndex != -1)
                    {
                        RankingValue = RankingValue + str[foundIndex + RankWore.Length + i];//current value 順位
                    }
                }
                  
                    searchWord = "normal yjSt\">"; //検索する文字列 企業名
                    foundIndex = str.IndexOf(searchWord);//始めの位置を探す
                    i = searchWord.Length;
                    for (; Convert.ToString(str[foundIndex + i]) != "<"; i++)
                    {
                        Name = Name + str[foundIndex + i];//previous 前日比? ¥
                    }

                    
                    searchWord = "txtright\">"; //検索する文字列 配当金
                    foundIndex = str.IndexOf(searchWord);//始めの位置を探す
                    i = searchWord.Length;//次のsearchWordに進める
                    foundIndex = foundIndex + i;
                    foundIndex = str.IndexOf(searchWord,foundIndex);//始めの位置を探す
                    for (; Convert.ToString(str[foundIndex + i]) != "<"; i++)//(－)下落
                    {
                        Dividend = Dividend + str[foundIndex + i];//(-)
                    }
                   




                    i++;
                    //for (; Convert.ToString(str[foundIndex + i]) != "）"; i++)
                    //{
                    //    PercentRatio = PercentRatio + str[foundIndex + i];//previous 前日比? %
                    //}


                    //if (RankingValue == "---")
                    //{
                    //    //tabledata.Realprice = 000;
                    //}
                    //else
                    //{
                    //    //tabledata.Realprice = Convert.ToDecimal(RankingValue);//現在値
                    //}
                    tabledata.RankingValue = RankingValue;//順位
                    tabledata.Name = Name; //企業名
                    tabledata.Dividend = Dividend;//配当金額
                    //tabledata.Gain = (tabledata.Realprice - tabledata.Itemprice) * tabledata.Stocks;//損益
                    //tabledata.RealValue = (tabledata.Stocks * tabledata.Realprice);//個別利益


                    RankingValue = "";
                    Name = "";
                    Dividend = "";
                    index = index + 1;
                }
                catch (Exception)
                {
                    //tabledata.Prev_day = "Close";
                    //tabledata.Polar = "Gray";
                }

        
            return tabledata;//polarity;

        }//class to end 

           
     
    }
}

