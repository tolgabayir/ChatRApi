using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR101.Models
{
	public class MessageNotification
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int UnReadedMessage { get; set; }
    }
}

