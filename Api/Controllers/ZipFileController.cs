using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Net;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ZipFileController : ControllerBase
{

    private readonly ILogger<ZipFileController> _logger;

    public ZipFileController(ILogger<ZipFileController> logger)
    {
        _logger = logger;
    }


    // Todo: Fix and Check
    [HttpPost(Name = "ZipFile")]    
    public HttpResponseMessage ReceiveZipFile(ZipArchive file)
    {
        if (Directory.Exists(@".\archive"))
        {
            Directory.Delete(@".\archive");
        }
        file.ExtractToDirectory(@".\archive");

        return new HttpResponseMessage(HttpStatusCode.OK);
    }
}