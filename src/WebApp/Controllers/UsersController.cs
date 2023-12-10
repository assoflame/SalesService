using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _services;

        public UsersController(IServiceManager services)
        {
            _services = services;
        }

        [HttpPost("{userId:int}/reviews")]
        public async Task<IActionResult> RateUser(int userId, [FromBody] ReviewCreationDto rateDto)
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

        [HttpGet("/api/chats")]
        public async Task<IActionResult> GetUserChats([FromQuery] ChatParameters chatParams)
        {
            if(int.TryParse(HttpContext?.User.FindFirst("Id")?.Value, out var userId))
            {
                var pagedChats = await _services.ChatService
                    .GetUserChatsAsync(userId, chatParams);

                Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedChats.metaData));

                Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

                return Ok(pagedChats.chatsDto);
            }

            return BadRequest();
        }

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

        [AllowAnonymous]
        [HttpGet("{userId:int}/reviews")]
        public async Task<IActionResult> GetUserReviews(int userId, [FromQuery] ReviewParams reviewParams)
        {
            var reviewsWithMetaData = await _services
                .UserService
                .GetUserReviewsAsync(userId, reviewParams);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(reviewsWithMetaData.metaData));

            Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");

            return Ok(reviewsWithMetaData.reviewsDto);
        }
    }
}
