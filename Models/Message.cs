using System;
using System.ComponentModel.DataAnnotations.Schema;
using SignalR101.Repository.Concrete.EfCore;

namespace SignalR101.Models
{
	public class Message
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
		public ApplicationUser Sender { get; set; }
		public ApplicationUser Receiver { get; set; }
		public string Text { get; set; }
		public string When { get; set; }
		public bool ReceiptInfo { get; set; }		
		public bool IsDeleted { get; set; }
    }
}

