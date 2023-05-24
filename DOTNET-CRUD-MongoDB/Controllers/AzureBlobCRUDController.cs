using DOTNET_CRUD_MongoDB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_CRUD_MongoDB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AzureBlobCRUDController : ControllerBase
    {
        private readonly IBlobStorage _storage;
        private readonly string _connectionString;
        private readonly string _container;

        public AzureBlobCRUDController(IBlobStorage storage, IConfiguration iConfig)
        {
            _storage = storage;
            _connectionString = iConfig.GetValue<string>("MyConfig:StorageConnection");
            _container = iConfig.GetValue<string>("MyConfig:ContainerName");
        }

        [HttpGet("ListFiles")]
        public async Task<List<string>> ListFiles()
        {
            return await _storage.GetAllDocuments(_connectionString, _container);
        }

        [Route("InsertFile")]
        [HttpPost]
        public async Task<bool> InsertFile([FromForm] IFormFile asset)
        {
            if (asset != null)
            {
                Stream stream = asset.OpenReadStream();
                await _storage.UploadDocument(_connectionString, _container, asset.FileName, stream);
                return true;
            }

            return false;
        }

        [HttpGet("DownloadFile/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var content = await _storage.GetDocument(_connectionString, _container, fileName);
            return File(content, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Route("DeleteFile/{fileName}")]
        [HttpGet]
        public async Task<bool> DeleteFile(string fileName)
        {
            return await _storage.DeleteDocument(_connectionString, _container, fileName);
        }
    }
}
