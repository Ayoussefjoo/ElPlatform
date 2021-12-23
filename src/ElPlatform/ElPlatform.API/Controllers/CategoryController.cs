using ElPlatform.BAL.IServices;
using ElPlatform.BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IMediaService _mediaService;
        private IConfiguration _configuration;
        public CategoryController(IMediaService mediaService, IConfiguration configuration)
        {
            _mediaService = mediaService;
            _configuration = configuration;
        }

        #region Media Item Category EndPoints
        //Media Item Category EndPoints
        // /api/media/GetMICatigories
        [HttpGet("GetMICatigories")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MediaItemCategoryVM>>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetGetMICatigoriesAsync()
        {
            var result = await _mediaService.GetMediaItemCategoriesAsync();

            return Ok(new ApiResponse<List<MediaItemCategoryVM>>(result, "Media Item Categories retrieved successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaItemCategoryVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediaService.GetMediaItemCategoryByIdAsync(id);

            return Ok(new ApiResponse<MediaItemCategoryVM>(result, "Media Item Category retrieved successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaItemCategoryVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpPost()]
        public async Task<IActionResult> Post([FromForm] MediaItemCategoryVM model)
        {
            var result = await _mediaService.AddMediaItemCategoryAsync(model);

            return Ok(new ApiResponse<MediaItemCategoryVM>(result, "Media Item Category created successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaItemCategoryVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpPut()]
        public async Task<IActionResult> Put([FromForm] MediaItemCategoryVM model)
        {
            var result = await _mediaService.UpdateMediaItemCategoryAsync(model);

            return Ok(new ApiResponse<MediaItemCategoryVM>(result, "Media Item Category edited successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediaService.DeleteMediaItemCategoryAsync(id);

            return Ok(new ApiResponse("Media Item Category deleted successfully"));
        }
        #endregion
    }
}
