using DTC.DefaultRepository.Exceptions;
using DTC.DefaultRepository.Helpers;
using Microsoft.AspNetCore.Mvc;
using NKTM.Constants;
using Project.Net8.Controllers.DefaultRepository;
using Project.Net8.Installers;
using Project.Net8.Interface.Core;
using Project.Net8.Models.Core;

namespace Project.Net8.Controllers.Core
{
    [Route("api/v1/[controller]")]
    // [Authorize]
    public class FilesController : DefaultReposityController<FileModel>
    {
        private readonly IFileService _service;
        private static string NameCollection = DefaultNameCollection.FILES;
 
        public FilesController(DataContext context, IFileService  service,  IHttpContextAccessor httpContextAccessor) : base(context, NameCollection,httpContextAccessor)
        {
            _service = service;
        }

        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            try
            {
                var data  = await _service.SaveFileAsync(files);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.CREATE_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }
        
        
        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [Route("upload-ck")]
        public async Task<IActionResult> UploadCK(IFormFile upload)
        {
            try
            {
                var data  = await _service.SaveFileAsync(upload);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.CREATE_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
        }
        
          [HttpGet]
        [Route("~/api/v1/files/view/{id}")]
        public async Task<IActionResult> ViewFile(string id)
        {
            try
            {
                var memory = await _service.GetFileById(id);

                
                
                return File(memory.data, "application/octet-stream", Path.GetFileName(memory.FileName));
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString).WithDetail(ex.Error)
                );
            }
           
         
        }


    }
}
