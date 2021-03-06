﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Twitter.Hashtag.Search.Entities;
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
        [Authorize]
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
            var messages = _twitterMessageService.RetrieveMessagesFromDatabase();
            return Ok(messages);
        }
    }
}