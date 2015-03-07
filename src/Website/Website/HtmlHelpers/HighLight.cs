using System;
using System.Text;
using System.Web.Mvc;

namespace Prekenweb.Website.HtmlHelpers
{
    public static partial class HtmlHelpers
    {
        public static MvcHtmlString HighLight(this HtmlHelper helper, string text, string word, int startPoint = 0)
        {
            if (startPoint >= 0 && !string.IsNullOrEmpty(word))
            {
                int startIndex = text.IndexOf(word, startPoint, StringComparison.InvariantCultureIgnoreCase);
                if (startIndex >= 0)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(text.Substring(0, startIndex));
                    builder.Append("<strong>");
                    builder.Append(text.Substring(startIndex, word.Length));
                    builder.Append("</strong>");
                    builder.Append(text.Substring(startIndex + word.Length));
                    return HighLight(helper, builder.ToString(), word, (startIndex + "<strong>".Length + "</strong>".Length + word.Length));
                }
            }
            return MvcHtmlString.Create(text);
        }

    }
}