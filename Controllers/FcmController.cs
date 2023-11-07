using System;
using Microsoft.AspNetCore.Mvc;
using SignalR101.Models;
using SignalR101.Services;

namespace SignalR101.Controllers
{
	[ApiController]
	[Route("Fcm")]
	public class FcmController:ControllerBase
	{
		private readonly FcmService fcmService;
		public FcmController(FcmService fcmService)
		{
			this.fcmService = fcmService;
		}

        [HttpPost("sendNotification")]
        public  async Task<IActionResult> SendNotification([FromBody] PushNotification request)
        {
            try
            {
                // Bildirimi FCM servisi kullanarak gönderin
               var res= await fcmService.SendPushNotificationAsync(request.DeviceToken, request.Title,request.Body);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ex.Message);
            }
        }

    }
}

