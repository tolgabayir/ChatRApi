using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR101.Models.Dto
{
	public class MessageDto
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string? Id { get; set; }
		public string SenderId { get; set; }
		public string ReceiverId { get; set; }
		public string Text { get; set; }
		public string When { get; set; }
		public bool ReceiptInfo { get; set; }
        public bool IsDeleted { get; set; }


	}
}

