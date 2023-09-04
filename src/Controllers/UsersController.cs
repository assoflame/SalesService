using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Security.Claims;
using System.Text.Json;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _services;

        public UsersController(IServiceManager services)
        {
            _services = services;
        }

        [Authorize]
        [HttpPost("{userId:int}/ratings")]
        public async Task<IActionResult> RateUser(int userId, [FromBody] RateDto rateDto)
        {
            if (rateDto is null)
                return BadRequest("rate dto object is null");

            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userWhoRateId) &&
                userId != userWhoRateId)
            {
                await _services.UserService.RateUser(userWhoRateId, userId, rateDto);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("messages/{userId:int}")]
        public async Task<IActionResult> SendMessage(int userId, [FromBody] MessageCreationDto messageCreationDto)
        {
            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userWhoSendsId))
            {
                var chat = await _services.ChatService.SendMessage(userWhoSendsId, userId, messageCreationDto);

                return Ok(chat);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet("/api/chats")]
        public async Task<IActionResult> GetUserChats()
        {
            if(int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                var chatsDto = await _services.ChatService.GetUserChats(userId);

                return Ok(chatsDto);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet("/api/chats/{chatId:int}")]
        public async Task<IActionResult> GetUserChat(int chatId)
        {
            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                var chat = await _services.ChatService.GetUserChat(userId, chatId);

                return Ok(chat);
            }

            return BadRequest();
        }

        [HttpGet("{userId:int}/ratings")]
        public async Task<IActionResult> GetUserRatings(int userId, [FromBody] RatingParameters ratingParams)
        {
            var ratingsWithMetaData = await _services
                .UserService
                .GetUserRatings(userId, ratingParams);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(ratingsWithMetaData.metaData));

            return Ok(ratingsWithMetaData.ratingsDto);
        }
    }
}
