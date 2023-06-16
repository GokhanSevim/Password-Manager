using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.MSSQL;
using Microsoft.Extensions.DependencyInjection;
using Notification.Abstract;
using Notification.Concrete;

namespace DependencyResolvers.Microsoft.MSSQL
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ISmtpSettingsDal, EfSmtpSettingsDal>();
            services.AddSingleton<IAddressesDal, EfAddressesDal>();
            services.AddSingleton<IBankAccountsDal, EfBankAccountsDal>();
            services.AddSingleton<ICategoriesDal, EfCategoriesDal>();
            services.AddSingleton<INotesDal, EfNotesDal>();
            services.AddSingleton<IPasswordsDal, EfPasswordsDal>();
            services.AddSingleton<IPaymentCardsDal, EfPaymentCardsDal>();

            services.AddSingleton<IEMailServices, EMailManager>();
            services.AddScoped<IAccountService, AccountManager>();
            services.AddSingleton<ISmtpSettingsService, SmtpSettingsManager>();
            services.AddSingleton<IAddressService, AddressManager>();
            services.AddSingleton<IBankAccountsService, BankAccountsManager>();
            services.AddSingleton<ICategoryService, CategoryManager>();
            services.AddSingleton<IPaymentCardsService, PaymentCardsManager>();
            services.AddSingleton<INotesService, NotesManager>();
            services.AddSingleton<IPasswordsService, PasswordsManager>();
        }
    }
}
