using AutoMapper;
using ElPlatform.BAL.Exceptions;
using ElPlatform.BAL.IServices;
using ElPlatform.BAL.Options;
using ElPlatform.BAL.ViewModels;
using ElPlatform.DAL;
using ElPlatform.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ElPlatform.BAL.Services
{
    public class MediaService : IMediaService
    {
        private IConfiguration _configuration;
        private readonly ElPlatformDbContext _db;
        private readonly IMapper _mapper;
        private readonly IdentityOptions _identity;
        public MediaService(IConfiguration configuration, ElPlatformDbContext db, IMapper mapper, IdentityOptions identity)
        {
            _configuration = configuration;
            _db = db;
            _mapper = mapper;
            _identity = identity;
        }
        public async Task<MediaItemVM> AddMediaItemAsync(MediaItemRequestVM model)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var mediaItem = _mapper.Map<MediaItem>(model);
                await _db.MediaItems.AddAsync(mediaItem);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<MediaItemVM>(mediaItem);

                ts.Complete();
                return result;
            }
        }
        public async Task DeleteMediaItemAsync(int Id)
        {
            var mediaItem = await _db.MediaItems.FindAsync(Id);
            if (mediaItem == null)
                throw new NotFoundException($"Media Item with the Id: {Id} not found");
            mediaItem.IsActive = true;
            mediaItem.ModifiedBy = _identity.UserId;
            mediaItem.ModifiedDate = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
        public async Task<MediaItemVM> GetMediaItemByIdAsync(int Id)
        {
            var mediaItem = await _db.MediaItems.FindAsync(Id);
            if (mediaItem == null)
                throw new NotFoundException($"Plan with the Id: {Id} cannot be found");

            return _mapper.Map<MediaItemVM>(mediaItem);
        }
        public async Task<PagedList<MediaItemVM>> GetMediaItemsAsync(int page = 1, int pageSize = 12)
        {
            var mediaItems = await (from p in _db.MediaItems
                                    where p.IsActive
                                    orderby p.CreatedDate descending
                                    select p).ToArrayAsync();
            var items= _mapper.Map<List<MediaItemVM>>(mediaItems);
            var pagedList = new PagedList<MediaItemVM>(items, page, pageSize);
            return pagedList;
        }
        public async Task<MediaItemVM> UpdateMediaItemAsync(MediaItemRequestVM model)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var mediaItem = await _db.MediaItems.FindAsync(model.Id);
                if (mediaItem == null)
                    throw new NotFoundException($"media item with the Id: {model.Id} not found");

                mediaItem.TitleEn = model.TitleEn;
                mediaItem.TitleAr = model.TitleAr;
                mediaItem.DescriptionEn = model.DescriptionEn;
                mediaItem.DescriptionAr = model.DescriptionAr;
                mediaItem.IsActive = model.IsActive;
                mediaItem.IsPublished = model.IsPublished;
                mediaItem.URL = model.URL;
                mediaItem.MediaTypeId = model.MediaTypeId;
                mediaItem.MediaCategoryId = model.MediaCategoryId;
                mediaItem.ModifiedBy = _identity.UserId;
                mediaItem.ModifiedDate = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                var result = _mapper.Map<MediaItemVM>(mediaItem);

                ts.Complete();
                return result;
            }
        }


        //Media Type services
        public async Task<PagedList<MediaTypeVM>> GetMediaTypesAsync(int page = 1, int pageSize = 5)
        {
            var mediaTypes = await (from p in _db.MediaTypes
                                    where p.IsActive
                                    orderby p.CreatedDate descending
                                    select p).ToArrayAsync();
            var items= _mapper.Map<List<MediaTypeVM>>(mediaTypes);
            var pagedList = new PagedList<MediaTypeVM>(items, page, pageSize);
            return pagedList;
        }
        public async Task<MediaTypeVM> GetMediaTypeByIdAsync(int Id)
        {
            var mediaType = await _db.MediaTypes.FindAsync(Id);
            if (mediaType == null)
                throw new NotFoundException($"Plan with the Id: {Id} cannot be found");

            return _mapper.Map<MediaTypeVM>(mediaType);
        }
        public async Task<MediaTypeVM> AddMediaTypeAsync(MediaTypeRequestVM model)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var mediaType = new MediaType();
                mediaType.NameEn = model.NameEn;
                mediaType.NameAr = model.NameAr;
                mediaType.IsActive = model.IsActive;
                mediaType.CreatedBy = _identity.UserId;
                mediaType.CreatedDate = DateTime.UtcNow;
                mediaType.ModifiedBy = _identity.UserId;
                mediaType.ModifiedDate = DateTime.UtcNow;
                await _db.MediaTypes.AddAsync(mediaType);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<MediaTypeVM>(mediaType);

                ts.Complete();
                return result;
            }
        }
        public async Task DeleteMediaTypeAsync(int Id)
        {
            var mediaType = await _db.MediaTypes.FindAsync(Id);
            if (mediaType == null)
                throw new NotFoundException($"Media Type with the Id: {Id} not found");
            mediaType.IsActive = false;
            mediaType.ModifiedBy = _identity.UserId;
            mediaType.ModifiedDate = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }
        public async Task<MediaTypeVM> UpdateMediaTypeAsync(MediaTypeVM model)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var mediaType = await _db.MediaTypes.FindAsync(model.Id);
                if (mediaType == null)
                    throw new NotFoundException($"Media Type with the Id: {model.Id} not found");

                mediaType.NameEn = model.NameEn;
                mediaType.NameAr = model.NameAr;
                mediaType.IsActive = model.IsActive;

                mediaType.ModifiedBy = _identity.UserId;
                mediaType.ModifiedDate = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                var result = _mapper.Map<MediaTypeVM>(mediaType);

                ts.Complete();
                return result;
            }
        }
        public async Task<List<MediaTypeVM>> GetAllMediaTypesAsync()
        {
            var mediaTypes = await (from p in _db.MediaTypes
                                    where p.IsActive
                                    orderby p.CreatedDate descending
                                    select p).ToArrayAsync();
            var items = _mapper.Map<List<MediaTypeVM>>(mediaTypes);
            return items;
        }

        //Media Item Category
        public async Task<PagedList<MediaItemCategoryVM>> GetMediaItemCategoriesAsync(int page = 1, int pageSize = 5)
        {
            var items = new List<MediaItemCategoryVM>();
            var mediaItemCategories = await(from p in _db.MediaItemCategories
                                   where p.IsActive
                                   orderby p.CreatedDate descending
                                   select p).ToArrayAsync();
            foreach (var item in mediaItemCategories)
            {
                items.Add(new MediaItemCategoryVM{
                Id=item.Id,
                NameAr=item.NameAr,
                NameEn=item.NameEn,
                IsActive=item.IsActive,
                ParantId=item.ParantId,
                ParantNameAr=item?.ParantCategory?.NameAr,
                ParantNameEn=item?.ParantCategory?.NameEn
                });
            };
            var pagedList = new PagedList<MediaItemCategoryVM>(items, page, pageSize);
            return pagedList;
        }
        public async Task<List<MediaItemCategoryVM>> GetMediaItemMainCategoriesAsync()
        {
            var items = new List<MediaItemCategoryVM>();
            var mediaItemCategories = await (from p in _db.MediaItemCategories
                                             where p.IsActive && p.ParantId==null
                                             orderby p.CreatedDate descending
                                             select p).ToArrayAsync();
            foreach (var item in mediaItemCategories)
            {
                items.Add(new MediaItemCategoryVM
                {
                    Id = item.Id,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                    IsActive = item.IsActive,
                    ParantId = item.ParantId,
                    ParantNameAr = item?.ParantCategory?.NameAr,
                    ParantNameEn = item?.ParantCategory?.NameEn
                });
            };
            return items;
         
        }
        public async Task<List<MediaItemCategoryVM>> GetMediaSubCategoriesAsync(int mainCategoryId)
        {
            var items = new List<MediaItemCategoryVM>();
            var mediaItemCategories = await (from p in _db.MediaItemCategories
                                             where p.IsActive && p.ParantId == mainCategoryId
                                             orderby p.CreatedDate descending
                                             select p).ToArrayAsync();
            foreach (var item in mediaItemCategories)
            {
                items.Add(new MediaItemCategoryVM
                {
                    Id = item.Id,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                    IsActive = item.IsActive,
                    ParantId = item.ParantId,
                    ParantNameAr = item?.ParantCategory?.NameAr,
                    ParantNameEn = item?.ParantCategory?.NameEn
                });
            };
            return items;
        }
        public async Task<MediaItemCategoryVM> GetMediaItemCategoryByIdAsync(int Id)
        {
            var mediaItemCategory = await _db.MediaItemCategories.FindAsync(Id);
            if (mediaItemCategory == null)
                throw new NotFoundException($"Media Item Category with the Id: {Id} cannot be found");

            return _mapper.Map<MediaItemCategoryVM>(mediaItemCategory);
        }

        public async Task<MediaItemCategoryVM> AddMediaItemCategoryAsync(MediaItemCategoryVM model)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var mediaItemCategory = new MediaItemCategory();
                mediaItemCategory.Id = model.Id;
                mediaItemCategory.NameAr = model.NameAr;
                mediaItemCategory.NameEn = model.NameEn;
                mediaItemCategory.IsActive = model.IsActive;
                if (model.ParantId==0)
                {
                    mediaItemCategory.ParantId = null;
                }
                else
                {
                    mediaItemCategory.ParantId = model.ParantId;
                }
              
                mediaItemCategory.CreatedBy = _identity.UserId;
                mediaItemCategory.CreatedDate = DateTime.UtcNow;
                mediaItemCategory.ModifiedBy = _identity.UserId;
                mediaItemCategory.ModifiedDate = DateTime.UtcNow;
                await _db.MediaItemCategories.AddAsync(mediaItemCategory);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<MediaItemCategoryVM>(mediaItemCategory);

                ts.Complete();
                return result;
            }
        }

        public async Task DeleteMediaItemCategoryAsync(int Id)
        {
            var mediaItemCategory = await _db.MediaItemCategories.FindAsync(Id);
            if (mediaItemCategory == null)
                throw new NotFoundException($"Media Item Category with the Id: {Id} not found");
            mediaItemCategory.IsActive = false;
            mediaItemCategory.ModifiedBy = _identity.UserId;
            mediaItemCategory.ModifiedDate = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        public async Task<MediaItemCategoryVM> UpdateMediaItemCategoryAsync(MediaItemCategoryVM model)
        {
            using (var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var mediaItemCategory = await _db.MediaItemCategories.FindAsync(model.Id);
                if (mediaItemCategory == null)
                    throw new NotFoundException($"Media Item Category with the Id: {model.Id} not found");

                mediaItemCategory.NameEn = model.NameEn;
                mediaItemCategory.NameAr = model.NameAr;
                mediaItemCategory.ParantId = model.ParantId;
                mediaItemCategory.IsActive = model.IsActive;
                mediaItemCategory.ModifiedBy = _identity.UserId;
                mediaItemCategory.ModifiedDate = DateTime.UtcNow;
                await _db.SaveChangesAsync();

                var result = _mapper.Map<MediaItemCategoryVM>(mediaItemCategory);

                ts.Complete();
                return result;
            }
        }
    }
}
