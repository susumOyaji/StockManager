using Xamarin.Forms;

namespace StockManager.Controls
{
    public sealed class PageDataTemplateSelector : DataTemplateSelector
    {

        public PageDataTemplateSelector()
        {
            
        }

        public DataTemplate Page1
        {
            get;
            set;
        }

        public DataTemplate Page2
        {
            get;
            set;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var viewModel = item as PageViewModel;
            if (viewModel == null)
                return null;

            return viewModel is Page1ViewModel ? this.Page1 : viewModel is Page2ViewModel ? this.Page2 : null;
        }
    }
}
