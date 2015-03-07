using Prekenweb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Web.Mvc.Ajax;

namespace Prekenweb.Website.Lib
{
    public static class HtmlHelpers
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

        public static MvcHtmlString Resource<T>(this HtmlHelper<T> html, string key)
        {
            var resourceManager = new ResourceManager(typeof(Resources.Resources));

            var val = resourceManager.GetString(key.Replace(" ", ""));

            // if value is not found return the key itself
            return MvcHtmlString.Create(String.IsNullOrEmpty(val) ? key : val);
        }

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
        public static MvcHtmlString SecuredActionLink(this HtmlHelper htmlHelper, string linkName, string actionName, string controllerName, object routeValues, object htmlAttributes, string rol, string icon = "")
        {
            if (!htmlHelper.ViewContext.HttpContext.Request.IsAuthenticated) return null;
            //HttpCookie cookie = htmlHelper.ViewContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (cookie != null)
            //{

            //    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            if (rol.Split(',').Any(r => htmlHelper.ViewContext.HttpContext.User.IsInRole(r)))
                if (string.IsNullOrEmpty(icon))
                {
                    return htmlHelper.ActionLink(linkName, actionName, controllerName, routeValues, htmlAttributes);
                }
                else
                {
                    return htmlHelper.IconActionLink(linkName, actionName, controllerName, routeValues, htmlAttributes, icon);
                }
            //}
            return null;
        }
        public static MvcHtmlString SecuredActionLinkLI(this HtmlHelper htmlHelper, string linkName, string actionName, string controllerName, object routeValues, object htmlAttributes, string rol, string icon = "")
        {

            var defaultLink = SecuredActionLink(htmlHelper, linkName, actionName, controllerName, routeValues, htmlAttributes, rol, icon);
            return defaultLink == null ? null : MvcHtmlString.Create("<li>" + defaultLink + "</li>");
        }

        public static Uri GetBaseUrl(this UrlHelper url)
        {
            var uri = new Uri(
                url.RequestContext.HttpContext.Request.Url,
                url.RequestContext.HttpContext.Request.RawUrl
            );
            var builder = new UriBuilder(uri)
            {
                Path = url.RequestContext.HttpContext.Request.ApplicationPath,
                Query = null,
                Fragment = null
            };
            return builder.Uri;
        }

        public static string ContentAbsolute(this UrlHelper url, string contentPath)
        {
            return new Uri(GetBaseUrl(url), url.Content(contentPath)).AbsoluteUri;
        }

        //public static MvcHtmlString MenuActionLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        //{
        //    if (helper.ViewContext.Controller.GetType().Name.Equals(controllerName + "Controller", StringComparison.OrdinalIgnoreCase))
        //    {
        //        htmlAttributes.Add("class", "current");
        //    }

        //    return helper.ActionLink(linkText, actionName, controllerName, new RouteValueDictionary(), htmlAttributes);
        //}

        public static string CheckedActionLink(this AjaxHelper helper, string linkTitle, string actionName, object routeValues, AjaxOptions ajaxOptions, bool @checked)
        {
            //<span class="fa-check-empty"></span><span class="fa-check"></span>
            var uncheckedSpan = new TagBuilder("span");
            uncheckedSpan.MergeAttribute("class", "fa fa-square-o");
            var checkedSpan = new TagBuilder("span");
            checkedSpan.MergeAttribute("class", "fa fa-check-square-o");
            var link = helper.ActionLink("blaat", actionName, routeValues, ajaxOptions, new { @class = @checked ? "checked" : "unchecked" });
            return link.ToString().Replace("blaat", string.Format("{0}{1}{2}", uncheckedSpan, checkedSpan, linkTitle));
        }
        public static MvcHtmlString IconActionLink(this HtmlHelper helper, string linkTitle, string actionName, string controllerName, object routeValues, object htmlAttributes, string icon)
        {
            var iconSpan = new TagBuilder("span");
            iconSpan.MergeAttribute("class", string.Format("fa fa-{0}", icon));

            RouteValueDictionary htmlAttributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            //if (htmlAttributesDictionary.Any(a => a.Key.ToLower() == "class")) htmlAttributesDictionary["class"] += " Knop IconKnop";
            //else htmlAttributesDictionary.Add("class", "IconKnop");

            var link = helper.ActionLink("{icon}", actionName, controllerName, new RouteValueDictionary(routeValues), htmlAttributesDictionary.ToDictionary(f => f.Key, f => f.Value));

            return new MvcHtmlString(link.ToString().Replace("{icon}", string.Format("{0} {1}", iconSpan, linkTitle)));
        }
        public static MvcHtmlString HiglightedActionLink(this HtmlHelper helper, string linkTitle, string actionName, string controllerName, object routeValues, object htmlAttributes, string textToHightlight)
        {
            var link = helper.ActionLink("{text}", actionName, controllerName, routeValues, htmlAttributes);

            return new MvcHtmlString(link.ToString().Replace("{text}", HighLight(helper, linkTitle, textToHightlight).ToString()));
        }
    }
}