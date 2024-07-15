using System.Threading.Tasks;
using RazorLight;
using System.IO;

namespace GestionMedicoBackend.Services.User
{
    public class EmailTemplateService
    {
        private readonly RazorLightEngine _razorLightEngine;

        public EmailTemplateService()
        {
            var templatesPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
            _razorLightEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(templatesPath)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderTemplateAsync<T>(string templateName, T model)
        {
            string result = await _razorLightEngine.CompileRenderAsync($"{templateName}.cshtml", model);
            return result;
        }

    }
}
