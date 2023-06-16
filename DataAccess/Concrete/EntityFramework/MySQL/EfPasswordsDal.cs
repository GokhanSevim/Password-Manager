using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.MySQL.Contexts;
using Entities;

namespace DataAccess.Concrete.EntityFramework.MySQL
{
    public class EfPasswordsDal : EfEntityRepositoryBase<EPasswords, MySqlDataContext>, IPasswordsDal
    {
    }
}
