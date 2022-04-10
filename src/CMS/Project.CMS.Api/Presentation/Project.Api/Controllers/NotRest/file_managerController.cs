using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Api.Helpers;
using Project.Api.ViewModels.FileManagerViewModels;


namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class file_managerController : BaseController
    {
        private readonly IFileManager _fileManager;

        public file_managerController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [Authorize]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload([FromForm] upload_file_vm model)
        {
            try
            {
                var result = await _fileManager.Upload(model);

                return Ok(result.message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("createdirectory")]
        public IActionResult CreateDirectory(string path)
        {
            var result = _fileManager.CreateDirectory(path);
            var jsonData = JsonConvert.SerializeObject(result);
            return CustomOk(jsonData);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetDirectories(string path = "/")
        {
            var result = await _fileManager.GetDirectory(path, true);

            return CustomOk(result.files.OrderByDescending(x => x.modified_date));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("removefile")]
        public IActionResult RemoveFile(string path)
        {
            var result = _fileManager.DeleteSingleFile(path);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("removedirectory")]
        public IActionResult RemoveDirectory(string path)
        {
            var result = _fileManager.RemoveDirectory(path);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("rename")]
        public IActionResult Rename(string path, string newPath)
        {
            var result = _fileManager.Rename(path, newPath);
            return Ok(result);
        }
    }
}
