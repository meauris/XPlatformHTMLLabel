using System.ComponentModel;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CYINT.XPlatformHTMLLabel;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(iOSHtmlLabelRenderer))]
namespace CYINT.XPlatformHTMLLabel
{
    public class iOSHtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null && Element != null && !string.IsNullOrWhiteSpace(Element.Text))
            {
                var prefFont = UIFont.GetPreferredFontForTextStyle(new NSString("UICTFontTextStyleBody"));
                UIStringAttributes attrBody = new UIStringAttributes
                {
                    Font = prefFont
                };
                var attr = new NSAttributedStringDocumentAttributes();
                var nsError = new NSError();
                attr.DocumentType = NSDocumentType.HTML;

                var myHtmlData = NSData.FromString(Element.Text, NSStringEncoding.Unicode);
                Control.Lines = 0;
                NSMutableAttributedString mutable = new NSMutableAttributedString(new NSAttributedString(myHtmlData, attr, ref nsError));
                
                mutable.AddAttributes(attrBody, new NSRange(0, mutable.Length));
                mutable.FixAttributesInRange(new NSRange(0, mutable.Length));
                
                Control.AttributedText = mutable;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Label.TextProperty.PropertyName)
            {
                if (Control != null && Element != null && !string.IsNullOrWhiteSpace(Element.Text))
                {
                    var attr = new NSAttributedStringDocumentAttributes();
                    var nsError = new NSError();
                    attr.DocumentType = NSDocumentType.HTML;

                    var myHtmlData = NSData.FromString(Element.Text, NSStringEncoding.Unicode);
                    Control.Lines = 0;
                    Control.AttributedText = new NSAttributedString(myHtmlData, attr, ref nsError);
                }
            }
        }
    }
}
