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
            var ConnectionString = "Data Source=WKWAE3955183\\JOOSERVER;Initial Catalog=ElPlatform;Integrated Security=True"; 
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
