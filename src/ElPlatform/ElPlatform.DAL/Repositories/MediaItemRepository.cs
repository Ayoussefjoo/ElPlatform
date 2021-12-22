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
    public class MediaItemRepository : BaseRepository<MediaItem>
    {
        public MediaItemRepository(IUnitOfWork unit) : base(unit)
        {

        }
    }
}
