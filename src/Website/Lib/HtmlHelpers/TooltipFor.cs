using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Data.Attributes;

namespace Prekenweb.Website.Lib.HtmlHelpers
{
    public static partial class HtmlHelpers
    {
        public static MvcHtmlString ToolTipFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string tooltipTekst = string.Empty;
            var expressionBody = (MemberExpression)expression.Body;
            Type type = expressionBody.Expression.Type;

            MetadataTypeAttribute[] metadataAttributes = (MetadataTypeAttribute[])type.GetCustomAttributes(typeof(MetadataTypeAttribute), true);

            if (!metadataAttributes.Any())
            {
                foreach (Attribute attribute in expressionBody.Expression.Type.GetProperty(expressionBody.Member.Name).GetCustomAttributes(false))
                {
                    if (typeof(TooltipAttribute) == attribute.GetType())
                    {
                        tooltipTekst = (attribute as TooltipAttribute).GetTooltipText();
                    }
                }
            }
            else
            {
                foreach (MetadataTypeAttribute metadataAttribute in metadataAttributes)
                {
                    if (metadataAttribute.MetadataClassType.GetProperty(expressionBody.Member.Name) == null) continue;

                    var metadataPropertyAttributes = metadataAttribute.MetadataClassType.GetProperty(expressionBody.Member.Name).GetCustomAttributes(false);
                    foreach (Attribute metadataPropertyAttribute in metadataPropertyAttributes)
                    {
                        if (typeof(TooltipAttribute) == metadataPropertyAttribute.GetType())
                        {
                            tooltipTekst = (metadataPropertyAttribute as TooltipAttribute).GetTooltipText();
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(tooltipTekst)) return MvcHtmlString.Create("");
            else
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes.Add("class", "fa fa-question-circle tooltip");
                tb.Attributes.Add("href", "/");
                tb.Attributes.Add("onclick", "return false;");
                tb.Attributes.Add("Title", tooltipTekst);
                return MvcHtmlString.Create(tb.ToString());

            }
        }
    }
}