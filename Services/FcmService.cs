using FirebaseAdmin.Messaging;


namespace SignalR101.Services
{
	public class FcmService
	{
        private FirebaseMessaging _messaging;

        public FcmService()
        {
            _messaging = FirebaseMessaging.DefaultInstance;
        }

        public async Task<string> SendPushNotificationAsync(string deviceToken,string title,string body) {

            var message = new Message
            {
                Token=deviceToken,
                Notification= new Notification
                {
                    Title=title,
                    Body=body,
                }
            };

          var res = await _messaging.SendAsync(message);
            return res;
        }
    }
}

