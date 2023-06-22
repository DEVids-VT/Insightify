using Insightify.Friendship.Models.Dtos;
using Insightify.Friendship.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace Insightify.Friendship.Controllers
{
    [ApiController]
    [Route("friendship")]
    [Authorize]
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpPost("requests")]
        public async Task<IActionResult> SendFriendRequest(FriendRequestDto friendRequestDto)
        {
            var request = await _friendshipService
                .SendFriendRequest(friendRequestDto.SenderId, friendRequestDto.ReceiverId);

            if (request)
            {
                return Ok(request);
            }

            return BadRequest("Unable to send friend request");
        }

        [HttpPut("requests/{requestId}/accept")]
        public async Task<IActionResult> AcceptFriendRequest(string requestId)
        {
            var accepted = await _friendshipService.AcceptFriendRequest(requestId);

            if (accepted)
            {
                return Ok("Friend request accepted");
            }

            return BadRequest("Unable to accept friend request");
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
            var unfriended = await _friendshipService.Unfriend(friendshipId);

            if (unfriended)
            {
                return Ok("Unfriended successfully");
            }

            return BadRequest("Unable to unfriend");
        }

        [HttpGet("{userId}/friends")]
        public async Task<IActionResult> GetFriends(string userId)
        {
            var friends = await _friendshipService.GetFriends(userId);

            return Ok(friends);
        }
    }
}
