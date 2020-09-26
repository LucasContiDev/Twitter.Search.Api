using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twitter.Hashtag.Search.Entities;
using Twitter.Hashtag.Search.Services.Abstraction;
using Twitter.Search.Services.Abstraction;

namespace Twitter.Hashtag.Search.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TwitterMessagesController : ControllerBase
    {
        private readonly ILogger<TwitterMessagesController> _logger;
        private readonly ITwitterMessageService _twitterMessageService;

        public TwitterMessagesController(ILogger<TwitterMessagesController> logger,
            ITwitterMessageService twitterMessageService)
        {
            _logger = logger;
            _twitterMessageService = twitterMessageService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TwitterMessage>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RequestTwitterMessagesByHashtag([FromForm] [Required] string hashtag)
        {
            var messages = await _twitterMessageService.GetMessagesByHashtag(hashtag);
            return Ok(messages);
        }

        [HttpGet]
        public ActionResult GetTwitterMessagesByHashtag()
        {
            // requisitar o serviço de busca (diretamente ao banco de dados) para retornar o resultado com os dados informados na requisição
            var rng = new Random();
            return Ok(rng);
        }
    }
}