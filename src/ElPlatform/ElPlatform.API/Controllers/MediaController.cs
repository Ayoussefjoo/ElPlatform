using ElPlatform.BAL.IServices;
using ElPlatform.BAL.Options;
using ElPlatform.BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private IMediaService _mediaService;
        private IConfiguration _configuration;
        public MediaController(IMediaService mediaService, IConfiguration configuration)
        {
            _mediaService = mediaService;
            _configuration = configuration;
        }

        #region Media Item EndPoints
        // /api/media/GetMItems
        [HttpGet("GetMItems")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PagedList<MediaItemVM>>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
       
        public async Task<IActionResult> GetMediaItemsAsync(int pageNumber, int pageSize)
        {
            var result = await _mediaService.GetMediaItemsAsync(pageNumber,pageSize);

            return Ok(new ApiResponse<PagedList<MediaItemVM>>(result, "MediaItems retrieved successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaItemVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediaService.GetMediaItemByIdAsync(id);

            return Ok(new ApiResponse<MediaItemVM>(result, "MediaItem retrieved successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaItemVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] MediaItemRequestVM model)
        {
            var result = await _mediaService.AddMediaItemAsync(model);

            return Ok(new ApiResponse<MediaItemVM>(result, "Media Item created successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaItemVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] MediaItemRequestVM model)
        {
            var result = await _mediaService.UpdateMediaItemAsync(model);

            return Ok(new ApiResponse<MediaItemVM>(result, "Media Item edited successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediaService.DeleteMediaItemAsync(id);

            return Ok(new ApiResponse("Media Item deleted successfully"));
        }
        #endregion
        


    }
}
