using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR101.Models;
using SignalR101.Repository.Abstract;

namespace SignalR101.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {

        private static Dictionary<string, string> connectedUsers = new Dictionary<string, string>();



        public async Task SendChatMessage(Message message)
        {
                   
            await Clients.All.SendAsync("sendAsyncMessage", message);

           /* OnConnectedAsync(message.Reciever.Id);
            if (connectedUsers.TryGetValue(message.Reciever.Id, out string connectionId)) {
                message.When = DateTime.Now.Date;
            }*/
                 
        }



        public  async Task OnConnectedAsync(string userId)
        {
          
            if (!string.IsNullOrEmpty(userId))
            {
                connectedUsers[userId] = Context.ConnectionId;
            }

            await base.OnConnectedAsync();
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string userId = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userId))
            {
                connectedUsers.Remove(userId);
            }

            await base.OnDisconnectedAsync(exception);
        }

    }

}


