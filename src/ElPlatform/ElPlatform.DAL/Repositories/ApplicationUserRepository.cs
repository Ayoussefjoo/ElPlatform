using ElPlatform.DAL.Infrastructure;
using ElPlatform.DAL.Infrastructure.Contract;
using ElPlatform.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElPlatform.DAL.Repositories
{
    public class ApplicationUserRepository :  BaseRepository<ApplicationUser>
    {
        public ApplicationUserRepository(IUnitOfWork unit) : base(unit)
        {

        }
    }
}
