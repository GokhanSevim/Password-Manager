using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.MSSQL.Contexts;
using Entities;


namespace DataAccess.Concrete.EntityFramework.MSSQL
{
    public class EfPaymentCardsDal : EfEntityRepositoryBase<EPaymentCards, MSSqlDataContext>, IPaymentCardsDal
    {
    }
}
