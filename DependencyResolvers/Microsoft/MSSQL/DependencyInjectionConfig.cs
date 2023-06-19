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
            services.AddScoped<ISmtpSettingsDal, EfSmtpSettingsDal>();
            services.AddScoped<IAddressesDal, EfAddressesDal>();
            services.AddScoped<IBankAccountsDal, EfBankAccountsDal>();
            services.AddScoped<ICategoriesDal, EfCategoriesDal>();
            services.AddScoped<INotesDal, EfNotesDal>();
            services.AddScoped<IPasswordsDal, EfPasswordsDal>();
            services.AddScoped<IPaymentCardsDal, EfPaymentCardsDal>();

            services.AddScoped<IEMailServices, EMailManager>();
            services.AddScoped<IAccountService, AccountManager>();
            services.AddScoped<ISmtpSettingsService, SmtpSettingsManager>();
            services.AddScoped<IAddressService, AddressManager>();
            services.AddScoped<IBankAccountsService, BankAccountsManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IPaymentCardsService, PaymentCardsManager>();
            services.AddScoped<INotesService, NotesManager>();
            services.AddScoped<IPasswordsService, PasswordsManager>();
        }
    }
}
