using AutoMapper;
using ElPlatform.BAL.Exceptions;
using ElPlatform.BAL.IServices;
using ElPlatform.BAL.ViewModels;
using ElPlatform.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.BAL.Services
{
    public class MediaService : IMediaService
    {
        private IConfiguration _configuration;
        private readonly ElPlatformDbContext _db;
        private readonly IMapper _mapper;
        public MediaService(IConfiguration configuration, ElPlatformDbContext db,IMapper mapper)
        {
            _configuration = configuration;
            _db = db;
            _mapper = mapper;
        }
        public Task<string> AddMediaItem(int model)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteMediaItem(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<MediaItemVM> GetMediaItemByIdAsync(int Id)
        {
            var mediaItem = await _db.MediaItems.FindAsync(Id);
            if (mediaItem == null)
                throw new NotFoundException($"Plan with the Id: {Id} cannot be found");

            return _mapper.Map<MediaItemVM>(mediaItem);
        }

        public async Task<List<MediaItemVM>> GetMediaItemsAsync()
        {
            var mediaItems = await(from p in _db.MediaItems
                              where  p.IsActive
                              orderby p.CreatedDate descending
                              select p).ToArrayAsync();
            return _mapper.Map<List<MediaItemVM>>(mediaItems);
        }

        public Task<string> UpdateMediaItem(int Id)
        {
            throw new NotImplementedException();
        }


        //Media Type services
        public async Task<List<MediaTypeVM>> GetMediaTypesAsync()
        {
            var mediaTypes = await(from p in _db.MediaTypes
                                   where p.IsActive
                                   orderby p.CreatedDate descending
                                   select p).ToArrayAsync();
            return _mapper.Map<List<MediaTypeVM>>(mediaTypes);
        }

        public async Task<MediaTypeVM> GetMediaTypeByIdAsync(int Id)
        {
            var mediaType = await _db.MediaTypes.FindAsync(Id);
            if (mediaType == null)
                throw new NotFoundException($"Plan with the Id: {Id} cannot be found");

            return _mapper.Map<MediaTypeVM>(mediaType);
        }
    }
}
