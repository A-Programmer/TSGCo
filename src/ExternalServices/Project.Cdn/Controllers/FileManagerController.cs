using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Cdn.Helpers;
using Project.Cdn.ViewModels.FileManagerViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Cdn.Controllers
{
    [Route("[controller]")]
    public class FileManagerController : Controller
    {
        private readonly IFileManager _fileManager;

        public FileManagerController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload([FromForm] UploadFileVm model)
        {
            try
            {
                var result = await _fileManager.Upload(model);

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("createdirectory")]
        public IActionResult CreateDirectory(string path)
        {
            var result = _fileManager.CreateDirectory(path);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectories(string path = "/")
        {
            var result = await _fileManager.GetDirectory(path, true);

            return Json(result);
        }

        [HttpDelete("removefile")]
        public IActionResult RemoveFile(string path)
        {
            var result = _fileManager.DeleteSingleFile(path);
            return Ok(result);
        }

        [HttpDelete("removedirectory")]
        public IActionResult RemoveDirectory(string path)
        {
            var result = _fileManager.RemoveDirectory(path);
            return Ok(result);
        }

        [HttpPut("rename")]
        public IActionResult Rename(string path, string newPath)
        {
            var result = _fileManager.Rename(path, newPath);
            return Ok(result);
        }
    }
}
