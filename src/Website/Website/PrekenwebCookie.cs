using System;
using System.Linq;
using System.Web;

namespace Prekenweb.Website.Lib
{
    public interface IPrekenwebCookie
    {
        bool FilterPreken { get; set; }
        bool FilterLeesPreken { get; set; }
        bool FilterLezingen { get; set; }
        bool WelkomstekstVerbergen { get; set; }
    }

    public class PrekenwebCookie : IPrekenwebCookie
    {
        private enum Key
        {
            FilterPreken,
            FilterLeesPreken,
            FilterLezingen,
            WelkomstekstVerbergen,
            CookieId
        }

        private static string _cookieName = "PrekenwebCookie";

        public bool FilterPreken { get { return getValue<bool>(Key.FilterPreken); } set { setValue<bool>(Key.FilterPreken, value); } }
        public bool FilterLeesPreken { get { return getValue<bool>(Key.FilterLeesPreken); } set { setValue<bool>(Key.FilterLeesPreken, value); } }
        public bool FilterLezingen { get { return getValue<bool>(Key.FilterLezingen); } set { setValue<bool>(Key.FilterLezingen, value); } }
        public bool WelkomstekstVerbergen { get { return getValue<bool>(Key.WelkomstekstVerbergen); } set { setValue<bool>(Key.WelkomstekstVerbergen, value); } }
        public string CookieId { get { return getValue<string>(Key.CookieId); } set { setValue<string>(Key.CookieId, value); } }
        public Guid CookieGuid { get { return Guid.Parse(CookieId); } set { } }

        public PrekenwebCookie()
        {

        }
        private T getValue<T>(Key key)
        {
            var prekenwebCookie = getHttpCookie();
            string value = prekenwebCookie[key.ToString()];

            if (typeof(T) == typeof(bool))
            {
                try
                {
                    return (T)(object)bool.Parse(value);
                }
                catch
                {
                    return default(T);
                }
            }
            if (typeof(T) == typeof(string))
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        switch (key)
                        {
                            case Key.CookieId:
                                return (T)(object)Guid.NewGuid().ToString();
                            default:
                                return (T)(object)string.Empty;
                        }
                    }
                    else return (T)(object)value;
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);
        }

        private void setValue<T>(Key key, T value)
        {
            var prekenwebCookie = getHttpCookie();
             prekenwebCookie.Expires = DateTime.Now.AddYears(1);

            if (typeof(T) == typeof(bool))
            {
                prekenwebCookie[key.ToString()] = ((bool)(object)value).ToString();
            }
            if (typeof(T) == typeof(string))
            {
                prekenwebCookie[key.ToString()] = (string)(object)value;
            }
            HttpContext.Current.Response.Cookies.Set(prekenwebCookie);
        }

        private HttpCookie getHttpCookie()
        { 
            var responseCookieExist = HttpContext.Current.Response.Cookies.AllKeys.Any(x => x == _cookieName);
            if (!responseCookieExist)
            {
                var requestCookie = HttpContext.Current.Request.Cookies[_cookieName];

                if (requestCookie != null && requestCookie.HasKeys) return requestCookie;
                else return createCookie();
            }
            return HttpContext.Current.Response.Cookies[_cookieName];
        }

        public HttpCookie createCookie()
        {
            HttpCookie prekenwebCookie = new HttpCookie(_cookieName);
            prekenwebCookie[Key.FilterPreken.ToString()] = Boolean.TrueString;
            prekenwebCookie[Key.FilterLeesPreken.ToString()] = Boolean.TrueString;
            prekenwebCookie[Key.FilterLezingen.ToString()] = Boolean.TrueString;
            prekenwebCookie[Key.CookieId.ToString()] = Guid.NewGuid().ToString();
            prekenwebCookie[Key.WelkomstekstVerbergen.ToString()] = Boolean.FalseString;
            prekenwebCookie.Expires = DateTime.Now.AddDays(365);
            HttpContext.Current.Response.Cookies.Remove("PrekenwebCookie"); // volgens mij niet meer nodig
            HttpContext.Current.Response.Cookies.Add(prekenwebCookie);
            return prekenwebCookie;
        }

    }
}