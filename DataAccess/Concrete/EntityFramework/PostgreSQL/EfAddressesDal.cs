using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.PostgreSQL.Contexts;
using Entities;
using System;

namespace DataAccess.Concrete.EntityFramework.PostgreSQL
{
    public class EfAddressesDal : EfEntityRepositoryBase<EAddresses, PostgreSqlDataContext>, IAddressesDal
    {
    }
}
