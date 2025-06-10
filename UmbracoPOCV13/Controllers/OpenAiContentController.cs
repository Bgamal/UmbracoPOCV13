using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.Controllers;

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
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = apiKey
            });

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser(request.Prompt)
                },
                Model = Models.Gpt_3_5_Turbo,
                MaxTokens = 100
            });

            if (completionResult.Successful)
            {
                return Ok(completionResult.Choices.First().Message.Content.Trim());
            }
            else
            {
                return StatusCode(500, completionResult.Error?.Message ?? "Unknown error from OpenAI");
            }
        }

        public class PromptRequest
        {
            public string Prompt { get; set; }
        }
    }
} 