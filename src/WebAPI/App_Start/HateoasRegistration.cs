using FluentHateoas.Contracts;
using FluentHateoas.Registration;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI
{
    public class HateoasRegistration : IHateoasRegistrationProfile
    {
        public void Register(IHateoasContainer container)
        {
            container.Configure(
                new
                {
                    HrefStyle = HrefStyle.Relative,
                    LinkStyle = LinkStyle.Array,
                    TemplateStyle = TemplateStyle.Rendered,
                    NullValueHandling = NullValueHandling.Ignore
                });

            container
                .RegisterCollection<BookViewModel>("self") // 'self' link op collection result
                .Get<BooksController>();

            container
                .Register<BookViewModel>("self", b => b.Id) // 'self' link op single book result
                .Get<BooksController>();

            container
                .Register<BookViewModel>("get-all") // 'get-all' link op single book result
                .Get<BooksController>();
        }
    }
}