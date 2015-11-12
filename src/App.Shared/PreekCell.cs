using Xamarin.Forms;

namespace App.Shared
{
    public class PreekCell : ViewCell
    {
        public PreekCell()
        {
            var label = new Label
            {
                YAlign = TextAlignment.Start,
               
            };
            label.SetBinding(Label.TextProperty, "PreekTitel"); 

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 20, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = { label/*, tick*/ }
            }; 
            View = layout;
        }

        protected override void OnBindingContextChanged()
        { 
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}
