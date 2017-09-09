using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UploadFilesLab.Helper;

namespace UploadFilesLab.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task Get()
        {
            using (var fileStream = System.IO.File.Open(@"C:\dotnet-sdk-2.0.0-win-gs-x64.exe", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                var httpClient = new HttpClient();
                var form = new MultipartFormDataContent();
                form.Add(new StringContent(@"{ ""id"": ""458"", ""name"": ""Mark Irland"" }"), "metadata");
                form.Add(new StreamContent(fileStream), "file", "file.exe");
                HttpResponseMessage response = await httpClient.PostAsync("http://localhost:11550/api/file", form);
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                string sd = response.Content.ReadAsStringAsync().Result;

            }
        }
        
        // POST api/values
        [HttpPost]
        public async Task Post()
        {
            FormValueProvider formModel;

            using (var stream = System.IO.File.Create("c:\\temp\\myfile.txt"))
            {
                formModel = await Request.StreamFile(stream);
            }
        }
    }
}
