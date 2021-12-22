using ElPlatform.DAL.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ElPlatform.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TransactionScope _transaction;
        private readonly ElPlatformDbContext _db;


        public UnitOfWork()
        {
            var ConnectionString = "Data Source=WKWAE3955183\\JOOSERVER;Initial Catalog=-PlannerApp;Integrated Security=True";
            //var ConnectionString = "Data Source=SQL6012.site4now.net;Initial Catalog=db_a67332_elplatform;User Id=db_a67332_elplatform_admin;Password=TcA@1234";
            var optionsBuilder = new DbContextOptionsBuilder<ElPlatformDbContext>();
            optionsBuilder.UseSqlServer(ConnectionString);
            _db = new ElPlatformDbContext(optionsBuilder.Options);

        }

        public void Dispose()
        {

        }

        public void StartTransaction()
        {

            _transaction = new TransactionScope();
        }

        public int Commit()
        {
            return _db.SaveChanges();
            // _transaction.Complete();
        }

        public DbContext Db
        {
            get { return _db; }
        }

    }
}
