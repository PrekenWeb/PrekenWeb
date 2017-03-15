using PrekenWeb.Data.Services.Interfaces;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services
{
    public class LanguagesService : ILanguagesService
    {
        public LanguageModel GetDefault()
        {
            return new LanguageModel { Id = 1 }; // TODO get default language from database
        }
    }
}