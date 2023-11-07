using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR101.Models;
using SignalR101.Models.Dto;
using SignalR101.Repository.Abstract;
using SignalR101.Repository.Concrete.EfCore;

namespace SignalR101.Controllers
{
	[ApiController]
	[Route("Chat")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ChatController:ControllerBase
	{

		private IMessageRepository messageRepository;
		private IMessageNotificationRepository messageNotificationRepository;

        private UserManager<ApplicationUser> userManager;


		public ChatController(IMessageRepository repository,
			UserManager<ApplicationUser> userManager,
            IMessageNotificationRepository messageNotificationRepository
            )
		{
			
			messageRepository = repository;
			this.userManager = userManager;
			this.messageNotificationRepository = messageNotificationRepository;

		}
	  

		[HttpPost]
		[Route("messages")]
		public async Task<List<MessageDto>> GetMessagesByUserId([FromBody]LoadModel loadModel)
		{
			var user = await userManager.FindByNameAsync(loadModel.Username);
			var query = messageRepository.GetAll()
				.Where(m => m.SenderId == user!.Id || m.ReceiverId == user.Id)
				.OrderByDescending(m => m.When)
				.Skip((loadModel.LoadNumber - 1) * loadModel.LoadSize)
				.Take(loadModel.LoadSize);

			var messages = query.ToList();

			return messages;
		}


		[HttpPost]
		[Route("message")]
		public async Task<IActionResult> SaveMessage([FromBody] Message message)
		{
		   	
			MessageDto messageDto = new()
			{
				SenderId = message.Sender.Id,
				ReceiverId = message.Receiver.Id,
				When = message.When,
				ReceiptInfo = message.ReceiptInfo,
				Text = message.Text,

			};

			var res= await messageRepository.AddAsync(messageDto);

			return res.IsSuccessful ? Ok("Message Saved") : BadRequest(res.Message);

		}



		[HttpPost]
		[Route("deleteMessageById")]
		public  ObjectResult DeleteMessage([FromBody] MessageIdDto dto)
		{
			
            var mes = messageRepository.Find(m => m.Id == dto.MessageId).FirstOrDefault();

            if (mes == null)
            {
                return NotFound("Message not found");
            }

            var res = messageRepository.Delete(mes);

            return res.IsSuccessful ? Ok(res) : BadRequest(res.Message);

        }

        [HttpPost]
        [Route("deleteMessagesById")]
        public ObjectResult DeleteMessages([FromBody] List<string> messageIds)
        {

			var messages = messageRepository.GetAll().Where(m => messageIds.Contains(m.Id)).ToList();
			var res=messageRepository.DeleteAll(messages);
            return res.IsSuccessful ? Ok(res) : BadRequest(res.Message);

        }


        [HttpPost]
		[Route("notification")]
		public async Task<IActionResult> SaveNotification([FromBody] MessageNotification notification)
		{
			var res= await messageNotificationRepository.AddAsync(notification);
			return res.IsSuccessful ? Ok("Notif Saved") : BadRequest(res.Message);
			
		}


		[HttpPost]
		[Route("notification")]
		public async Task<ObjectResult> GetNotifications([FromBody] string username)
		{
			var user = await userManager.FindByNameAsync(username);
			var res = messageNotificationRepository.Get(user!.Id);
			return  res.IsSuccessful ? Ok("Success") : BadRequest(res.Message);
        }

	}
}

