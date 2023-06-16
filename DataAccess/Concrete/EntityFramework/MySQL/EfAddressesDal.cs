using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.MySQL.Contexts;
using Entities;
using System;

namespace DataAccess.Concrete.EntityFramework.MySQL
{
    public class EfAddressesDal : EfEntityRepositoryBase<EAddresses, MySqlDataContext>, IAddressesDal
    {
    }
}
