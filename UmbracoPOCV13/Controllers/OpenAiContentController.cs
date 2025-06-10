using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Controllers;

namespace UmbracoPOCV13.Controllers
{
    [Route("umbraco/api/[controller]/[action]")]
    public class OpenAiContentController : UmbracoApiController
    {
        [HttpPost]
        public async Task<IActionResult> Generate([FromBody] PromptRequest request)
        {
            var api = new OpenAIAPI("YOUR_OPENAI_API_KEY");
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