using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.PostgreSQL.Contexts;
using Entities;

namespace DataAccess.Concrete.EntityFramework.PostgreSQL
{
    public class EfSmtpSettingsDal : EfEntityRepositoryBase<ESmtpSettings, PostgreSqlDataContext>, ISmtpSettingsDal
    {
    }
}
