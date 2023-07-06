using Insightify.Friendships.Models.Dtos;
using Insightify.Friendships.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Friendships.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto friendRequestDto)
        {
            await _friendshipService
                .SendFriendRequest(friendRequestDto.SenderId, friendRequestDto.ReceiverId);

            return Ok("Friend request sent");
        }

        [HttpPut("requests/{requestId}/accept")]
        public async Task<IActionResult> AcceptFriendRequest(string requestId)
        {
            await _friendshipService.AcceptFriendRequest(requestId);

            return Ok("Friend request accepted");
        }

        [HttpPut("requests/{requestId}/reject")]
        public async Task<IActionResult> RejectFriendRequest(string requestId)
        {
            await _friendshipService.RejectFriendRequest(requestId);

            return Ok("Friend request rejected");
        }

        [HttpDelete("{friendshipId}")]
        public async Task<IActionResult> Unfriend(string friendshipId)
        {
            await _friendshipService.Unfriend(friendshipId);

            return Ok("Unfriended successfully");

        }

        [HttpGet("{userId}/requests")]
        public async Task<IActionResult> GetRequests(string userId, bool includeDeleted = false)
        {
            var requests = await _friendshipService.GetRequests(userId, includeDeleted);

            return Ok(requests);
        }

        [HttpGet("/requests")]
        public async Task<IActionResult> GetAllRequests(bool includeDeleted = false)
        {
            var requests = await _friendshipService.AllRequests(includeDeleted);

            return Ok(requests);
        }

        [HttpGet("{userId}/friends")]
        public async Task<IActionResult> GetFriendships(string userId, bool includeDeleted = false)
        {
            var friends = await _friendshipService.GetFriendships(userId, includeDeleted);

            return Ok(friends);
        }

        [HttpGet("/friendships")]
        public async Task<IActionResult> GetAllFriendships(bool includeDeleted = false)
        {
            var friendships = await _friendshipService.AllFriendships(includeDeleted);

            return Ok(friendships);
        }
    }
}
