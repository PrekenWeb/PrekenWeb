using System;

namespace PrekenWeb.Data.Attributes
{
    public class TooltipAttribute : Attribute
    {
        const string localization_failed_message = "Cannot tooltip because localization failed. Type '{0} is not public or does not contain a public static string property with the name '{1}'.";

        public Type ResourceType { get; set; }
        public string Name { get; set; }

        public TooltipAttribute()
        {
        }
        public TooltipAttribute(string name)
        {
            Name = name;
        }

        private string GetLocalizedString(string key)
        {
            // If we don't have a resource or a key, go ahead and fall back on the key
            if (ResourceType == null || key == null)
                return key;

            var property = ResourceType.GetProperty(key);

            // Strings are only valid if they are public static strings
            var isValid = false;
            if (ResourceType.IsVisible && property != null && property.PropertyType == typeof(string))
            {
                var getter = property.GetGetMethod();

                // Gotta have a public static getter on the property
                if (getter != null && getter.IsStatic && getter.IsPublic)
                {
                    isValid = true;
                }
            }

            // If it's not valid, go ahead and throw an InvalidOperationException
            if (!isValid)
            {
                var message = string.Format(localization_failed_message, ResourceType.ToString(), key);
                throw new InvalidOperationException(message);
            }

            return (string)property.GetValue(null, null);

        }
        public string GetTooltipText()
        {
            return GetLocalizedString(Name);
        }

    }
}