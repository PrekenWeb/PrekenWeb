namespace PrekenWeb.Security
{
    using Data;
    using Data.Identity;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;

    public class PrekenWebUserManager : UserManager<Gebruiker, int>, IPrekenWebUserManager
    {
        public PrekenWebUserManager(IUserStore<Gebruiker, int> store)
            : base(store)
        {
        }

        public static PrekenWebUserManager Create(IdentityFactoryOptions<PrekenWebUserManager> options, IOwinContext context)
        {
            var manager = new PrekenWebUserManager(new UserStore<Gebruiker, PrekenWebRole, int, PrekenWebUserLogin, PrekenWebUserRole, PrekenWebUserClaim>(context.Get<PrekenwebContext>()));

            manager.UserValidator = new UserValidator<Gebruiker, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<Gebruiker, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            manager.EmailService = new EmailIdentityMessageService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<Gebruiker, int>(dataProtectionProvider.Create("PrekenWeb Identity"));
            }
            manager.PasswordHasher = new PrekenWebPasswordHasher();
            return manager;
        }
    }
}