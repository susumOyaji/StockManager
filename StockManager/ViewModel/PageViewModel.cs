//using Microsoft.Practices.Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System;


namespace StockManager
{
    public abstract class PageViewModel : BindableBase
    {

        private string _Title1;
        public string Title1
        {
            get
            {
                return this._Title1;
            }
            set
            {
                this.SetProperty(ref this._Title1, value);
            }
        }


        private string _Title2;
        public string Title2
        {
            get
            {
                return this._Title2;
            }
            set
            {
                this.SetProperty(ref this._Title2, value);
                //this.OnPropertyChanged(nameof(Title2));

            }
        }


        private string _Title3;
        public string Title3
        {
            get
            {
                return this._Title3;
            }
            set
            {
                this.SetProperty(ref this._Title3, value);
            }
        }



        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this.SetProperty(ref this._name, value);
            }
        }

        private decimal _stocks;
        public decimal Stocks
        {
            get
            {
                return this._stocks;
            }
            set
            {
                this.SetProperty(ref this._stocks, value);
            }
        }

        private decimal _itemprice;
        public decimal Itemprice
        {
            get
            {
                return this._itemprice;
            }
            set
            {
                this.SetProperty(ref this._itemprice, value);
            }
        }


        private string _prev_day;
        public string Prev_day
        {
            get
            {
                return this._prev_day;
            }
            set
            {
                this.SetProperty(ref this._prev_day, value);
            }
        }

        private decimal _realprice;
        public decimal Realprice
        {
            get
            {
                return this._realprice;
            }
            set
            {
                this.SetProperty(ref this._realprice, value);
            }
        }

        private decimal _realvalue;
        public decimal RealValue
        {
            get
            {
                return this._realvalue;
            }
            set
            {
                this.SetProperty(ref this._realvalue, value);
            }
        }

        private string _firstLastName;
        public string FirstLastName
        {
            get
            {
                return this._firstLastName;
            }
            set
            {
                this.SetProperty(ref this._firstLastName, value);
            }
        }




        private string _percent;
        public string Percent
        {
            get
            {
                return this._percent;
            }
            set
            {
                this.SetProperty(ref this._percent, value);
            }
        }

        private decimal _gain;
        public decimal Gain
        {
            get
            {
                return this._gain;
            }
            set
            {
                this.SetProperty(ref this._gain, value);
            }
        }

        private int _idindex;
        public int Idindex
        {
            get
            {
                return this._idindex;
            }
            set
            {
                this.SetProperty(ref this._idindex, value);
            }
        }

        private string _polar;
        public string Polar
        {
            get
            {
                return this._polar;
            }
            set
            {
                this.SetProperty(ref this._polar, value);
            }
        }

        private string _rankingValue;
        public string RankingValue
        {
            get
            {
                return this._rankingValue;
            }
            set
            {
                this.SetProperty(ref this._rankingValue, value);
            }
        }

        private string _dividend;
        public string Dividend
        {
            get
            {
                return this._dividend;
            }
            set
            {
                this.SetProperty(ref this._dividend, value);
            }
        }


    }





    public sealed class Page1ViewModel : PageViewModel
    {

        public Page1ViewModel()
        {
           
        }

    }

    public sealed class Page2ViewModel : PageViewModel
    {
        public Page2ViewModel()
        {

        }

    }
      

}

