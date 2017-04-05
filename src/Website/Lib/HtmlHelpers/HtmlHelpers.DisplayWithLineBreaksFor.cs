using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    {
        public static MvcHtmlString DisplayWithLineBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model)
                .Replace("\n", "<br />\n")
                .Replace("\r\n", "<br />\r\n");

            return string.IsNullOrEmpty(model) ? MvcHtmlString.Empty : MvcHtmlString.Create(model);
        }
    }
}