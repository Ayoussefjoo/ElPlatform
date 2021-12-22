using ElPlatform.BAL.IServices;
using ElPlatform.BAL.ViewModels;
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

        // /api/media/GetMItems
        [HttpGet("GetMItems")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MediaItemVM>>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetMediaItemsAsync()
        {
            var result = await _mediaService.GetMediaItemsAsync();

            return Ok(new ApiResponse<List<MediaItemVM>>(result, "MediaItems retrieved successfully"));
        }

        // /api/media/GetMItemById
        [HttpGet("GetMItemById")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaItemVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetMediaItemsAsync(int Id)
        {
            var result = await _mediaService.GetMediaItemByIdAsync(Id);

            return Ok(new ApiResponse<MediaItemVM>(result, "MediaItem retrieved successfully"));
        }

        //Media Type EndPoints
        // /api/media/GetMTypes
        [HttpGet("GetMTypes")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<List<MediaTypeVM>>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetMediaTypesAsync()
        {
            var result = await _mediaService.GetMediaTypesAsync();

            return Ok(new ApiResponse<List<MediaTypeVM>>(result, "MediaTypes retrieved successfully"));
        }

        // /api/media/GetMItemById
        [HttpGet("GetMTypeById")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaTypeVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetMediaTypesAsync(int Id)
        {
            var result = await _mediaService.GetMediaTypeByIdAsync(Id);

            return Ok(new ApiResponse<MediaTypeVM>(result, "Media Type retrieved successfully"));
        }

    }
}
