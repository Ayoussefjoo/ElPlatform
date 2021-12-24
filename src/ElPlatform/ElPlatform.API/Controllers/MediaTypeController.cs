using AutoMapper;
using ElPlatform.BAL.IServices;
using ElPlatform.BAL.Options;
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
    public class MediaTypeController : ControllerBase
    {
        private IMediaService _mediaService;
        private IConfiguration _configuration;
        public MediaTypeController(IMediaService mediaService, IConfiguration configuration)
        {
            _mediaService = mediaService;
            _configuration = configuration;
        }

        #region Media Type EndPoints
        //Media Type EndPoints
        // /api/media/GetMTypes
        [HttpGet("GetMTypes")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PagedList<MediaTypeVM>>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetMediaTypesAsync(int pageNumber, int pageSize)
        {
            var result = await _mediaService.GetMediaTypesAsync(pageNumber,pageSize);

            return Ok(new ApiResponse<PagedList<MediaTypeVM>>(result, "MediaTypes retrieved successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaTypeVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediaService.GetMediaTypeByIdAsync(id);

            return Ok(new ApiResponse<MediaTypeVM>(result, "Media Type retrieved successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaTypeVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpPost()]
        public async Task<IActionResult> Post([FromForm] MediaTypeVM model)
        {
            var result = await _mediaService.AddMediaTypeAsync(model);

            return Ok(new ApiResponse<MediaTypeVM>(result, "Media Type created successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse<MediaTypeVM>))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpPut()]
        public async Task<IActionResult> Put([FromForm] MediaTypeVM model)
        {
            var result = await _mediaService.UpdateMediaTypeAsync(model);

            return Ok(new ApiResponse<MediaTypeVM>(result, "Media Type edited successfully"));
        }

        [ProducesResponseType(200, Type = typeof(ApiResponse))]
        [ProducesResponseType(400, Type = typeof(ApiErrorResponse))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediaService.DeleteMediaTypeAsync(id);

            return Ok(new ApiResponse("Media Type deleted successfully"));
        }
        #endregion
    }
}
