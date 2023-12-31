﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.MSSQL.Contexts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.MSSQL
{
    public class EfBankAccountsDal : EfEntityRepositoryBase<EBankAccounts, MSSqlDataContext>, IBankAccountsDal
    {
    }
}
