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
    class MediaTypeRepository : BaseRepository<MediaType>
    {
        public MediaTypeRepository(IUnitOfWork unit) : base(unit)
        {

        }
    }
}
