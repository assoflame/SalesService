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
        public async Task<IActionResult> RateUser(int userId, [FromBody] RatingCreationDto rateDto)
        {
            if (rateDto is null)
                return BadRequest("rate dto object is null");

            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userWhoRateId) &&
                userId != userWhoRateId)
            {
                await _services.UserService.RateUserAsync(userWhoRateId, userId, rateDto);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("{userId:int}/messages")]
        public async Task<IActionResult> SendMessage(int userId, [FromBody] MessageCreationDto messageCreationDto)
        {
            if (int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userWhoSendsId))
            {
                var message = await _services.ChatService.SendMessageAsync(userWhoSendsId, userId, messageCreationDto);

                return Ok(message);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet("/api/chats")]
        public async Task<IActionResult> GetUserChats()
        {
            if(int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                var chatsDto = await _services.ChatService.GetUserChatsAsync(userId);

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
                var chat = await _services.ChatService.GetUserChatAsync(userId, chatId);

                return Ok(chat);
            }

            return BadRequest();
        }

        [HttpGet("{userId:int}/ratings")]
        public async Task<IActionResult> GetUserRatings(int userId, [FromBody] RatingParameters ratingParams)
        {
            var ratingsWithMetaData = await _services
                .UserService
                .GetUserRatingsAsync(userId, ratingParams);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(ratingsWithMetaData.metaData));

            return Ok(ratingsWithMetaData.ratingsDto);
        }
    }
}
