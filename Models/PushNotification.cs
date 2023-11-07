using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR101.Models
{
	public class PushNotification
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string Title { get; set; }
		public string Body { get; set; }
		public string DeviceToken { get; set; }
	}
}

