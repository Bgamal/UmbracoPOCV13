using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Controllers;
using Microsoft.Extensions.Configuration;

namespace UmbracoPOCV13.Controllers
{
    [Route("umbraco/api/[controller]/[action]")]
    public class OpenAiContentController : UmbracoApiController
    {
        private readonly IConfiguration _configuration;
        public OpenAiContentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Generate([FromBody] PromptRequest request)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];
            var api = new OpenAIAPI(apiKey);
            var result = await api.Completions.CreateCompletionAsync(
                new OpenAI_API.Completions.CompletionRequest(request.Prompt, model: "text-davinci-003", max_tokens: 100)
            );
            return Ok(result.Completions[0].Text.Trim());
        }

        public class PromptRequest
        {
            public string Prompt { get; set; }
        }
    }
} 